using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application
{
    public interface ISubsidiaryApplication : IApplication<SubsidiaryViewModel>
    {
        SubsidiaryViewModel CheckDomainName(string domainName);
    }
}
