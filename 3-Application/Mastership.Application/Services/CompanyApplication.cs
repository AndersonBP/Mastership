using AutoMapper;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Domain.ViewModels.RequestResponseViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mastership.Application.Services
{
    public class CompanyApplication : BaseApplication<CompanyViewModel, CompanyDTO, ICompanyRepository>, ICompanyApplication
    {
        private readonly ICompanyRepository _repository;
        public CompanyApplication(ICompanyRepository repository, IMapper mapper, IUserDataService userDataService) : base(repository, mapper, userDataService)
        {
            this._repository = repository;
        }

        public CompanyViewModel CheckDomainName(string domainName)
        {
            var subsidiary = this.MapToViewModel(this._repository.GetByDomainName(domainName));
            if (subsidiary == null)
                throw new NotFoundException("Company not found!");

            return subsidiary;
        }
      
    }
}
