using Mastership.Domain.DTO;
using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application
{
    public interface ISubsidiaryApplication : IApplication<SubsidiaryViewModel>
    {
        SubsidiaryDTO GetSubsidiaryByUser(UserDTO user);
    }
}
