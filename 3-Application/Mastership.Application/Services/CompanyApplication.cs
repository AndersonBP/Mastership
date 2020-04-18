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
using System.Linq;
using System.Net;

namespace Mastership.Application.Services
{
    public class CompanyApplication : BaseApplication<CompanyViewModel, CompanyDTO, ICompanyRepository>, ICompanyApplication
    {
        private readonly ICompanyRepository _repository;
        private readonly ICompanyIpRangesRepository _companyIpRangesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyApplication(ICompanyRepository repository, IMapper mapper, IUserDataService userDataService, IHttpContextAccessor httpContextAccessor, ICompanyIpRangesRepository companyIpRangesRepository) : base(repository, mapper, userDataService)
        {
            this._repository = repository;
            this._httpContextAccessor = httpContextAccessor;
            this._companyIpRangesRepository = companyIpRangesRepository;
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
