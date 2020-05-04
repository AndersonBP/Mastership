using AutoMapper;
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
using Microsoft.AspNetCore.Mvc;
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
                        afdparams.Subsidiary = subsidiary.Id;
                        var afd = this._subsidiaryApplication.CreateAFD(afdparams);
                        byte[] bytes = afd.StreamBytes();

                        var ftpServerUrl = $"ftp://{company.Settings.FTPHost}/{company.Settings.FTPPath}/{afd.FileDownloadName}.txt";
                        var request = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.UsePassive = false;
                        request.Credentials = new NetworkCredential(company.Settings.FTPUser, company.Settings.FTPPass);
                        request.UseBinary = false;
                        request.ContentLength = bytes.Length;
                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                    catch (WebException ex)
                    {
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
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
