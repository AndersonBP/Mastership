using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class CompanyApplication : BaseApplication<CompanyViewModel, CompanyDTO, ICompanyRepository>, ICompanyApplication
    {
        public CompanyApplication(ICompanyRepository repository, IMapper mapper, IUserDataService userDataService) : base(repository, mapper, userDataService) { }

    }
}
