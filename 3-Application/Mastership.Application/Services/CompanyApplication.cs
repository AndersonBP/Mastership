using AutoMapper;
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class CompanyApplication : BaseApplication<CompanyViewModel, CompanyEntity, ICompanyRepository>, ICompanyApplication
    {
        public CompanyApplication(ICompanyRepository repository, IMapper mapper) : base(repository, mapper) { }

    }
}
