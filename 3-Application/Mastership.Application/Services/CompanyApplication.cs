using AutoMapper;
using FluentFTP;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.Exceptions;
using Mastership.Domain.Interfaces;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Domain.ViewModels.RequestResponseViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Mastership.Application.Services
{
    public class CompanyApplication : BaseApplication<CompanyViewModel, CompanyDTO, ICompanyRepository>, ICompanyApplication
    {
        private readonly ICompanyIpRangesRepository _companyIpRangesRepository;
        private readonly ISubsidiaryApplication _subsidiaryApplication;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyApplication(ICompanyRepository repository, IMapper mapper, IUserDataService userDataService, IHttpContextAccessor httpContextAccessor, ICompanyIpRangesRepository companyIpRangesRepository, ISubsidiaryApplication subsidiaryApplication) : base(repository, mapper, userDataService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._companyIpRangesRepository = companyIpRangesRepository;
            this._subsidiaryApplication = subsidiaryApplication;
        }

        public void AFDScheduled()
        {
            var afdparams = new AFDViewModel() { Start = DateTime.Now.AddDays(-1).AbsoluteStart(), End = DateTime.Now.AddDays(-1).AbsoluteEnd() };
            var comapnies = this._repository.WithAfdScheduled();
            foreach (var company in comapnies)
            {
                foreach (var subsidiary in company.Subsidiaries)
                {
                    try
                    {
                        var ftpServerUrl = "";
                        afdparams.Subsidiary = subsidiary.Id;
                        var afd = this._subsidiaryApplication.CreateAFD(afdparams);
                        var sucess = false;
                        var limit = 3500;
                        while (!sucess || limit <= 0)
                        {
                            try
                            {
                                byte[] bytes = afd.Buffer();
                                ftpServerUrl = $"ftp://{company.Settings.FTPHost}:21//{company.Settings.FTPPath}/{afd.FileDownloadName}{DateTime.Now.ToTimeStringNumbers()}.txt".Replace(" ", "");
                                var request = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
                                request.Method = WebRequestMethods.Ftp.UploadFile;
                                request.Timeout = 90000;
                                request.ReadWriteTimeout = 90000;
                                request.KeepAlive = false;
                                request.UseBinary = true;
                                request.Credentials = new NetworkCredential(company.Settings.FTPUser, company.Settings.FTPPass);
                                request.ContentLength = bytes.Length;
                                var requestStream = request.GetRequestStream();
                                requestStream.Write(bytes, 0, bytes.Length);
                                requestStream.Flush();
                                requestStream.Close();
                                sucess = true;
                            }
                            catch (Exception ex)
                            {
                                limit--;
                                if (limit <= 0)
                                    throw ex;

                                continue;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public void SendFileFtp(Stream buffer, string host, string user, string pass, string path)
        {
            FtpClient client = new FtpClient(host, new NetworkCredential(user, pass));
            client.ActivePorts = new int[] { 60500, 60000 }.ToList();
            client.DataConnectionType = FtpDataConnectionType.PASV;
            client.Connect();


            // upload a file and retry 3 times before giving up
            client.RetryAttempts = 3;
            client.Upload(buffer, path, existsMode: FtpRemoteExists.Overwrite, createRemoteDir: true);

            client.Disconnect();
        }

        public CheckDomainNameViewModel CheckDomainName(string domainName)
        {
            var companydb = this._repository.GetByDomainName(domainName);
            if (companydb == null)
                throw new NotFoundException("Company not found!");
            if (companydb.Settings != null && companydb.Settings.UseIpFilter)
            {
                var ipRanges = this._companyIpRangesRepository.GetByCompany(companydb.Id);
                var clientIp = this._httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToInteger();
                var rangeInt = ipRanges.Where(x => x.Enable).Select(x => new { Begin = x.Begin.ToInteger(), End = x.End.ToInteger() }).ToList();
                if (!rangeInt.Any(x => clientIp >= x.Begin && clientIp <= x.End) && !clientIp.Equals(1))
                    throw new NetworkException("IP not authorized!");
            }
            return new CheckDomainNameViewModel()
            {
                Name = companydb.Name,
                RazaoSocial = companydb.RazaoSocial,
                DomainName = companydb.DomainName,
                CNPJ = companydb.CNPJ,
                Adress = companydb.Adress,
                Image = companydb.Image,
                ZipCode = companydb.ZipCode,
                ServerTime = DateTime.Now,
                Enable = companydb.Enable
            };
        }
    }
}
