using AutoMapper;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Domain.Interfaces;
using Mastership.Domain.DTO;

namespace Mastership.Application.Services
{
    public class CompanyIpRangesApplication : BaseApplication<CompanyIpRangesViewModel, CompanyIpRangesDTO, ICompanyIpRangesRepository>, ICompanyIpRangesApplication
    {
        public CompanyIpRangesApplication(ICompanyIpRangesRepository repository, IMapper mapper, IUserDataService userDataService) : base(repository, mapper, userDataService) { }

    }
}
