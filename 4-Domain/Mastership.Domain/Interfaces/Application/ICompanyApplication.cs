using Mastership.Domain.ViewModels;
using Mastership.Domain.ViewModels.RequestResponseViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mastership.Domain.Interfaces.Application
{
    public interface ICompanyApplication : IApplication<CompanyViewModel> {
        CompanyViewModel CheckDomainName(string domainName);

    }
}
