using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application
{
    public interface ICompanyApplication : IApplication<CompanyViewModel> {
        CompanyViewModel CheckDomainName(string domainName);

    }
}
