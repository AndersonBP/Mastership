using Mastership.Domain.DTO;
using Mastership.Domain.ViewModels;
using Mastership.Domain.ViewModels.RequestResponseViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mastership.Domain.Interfaces.Application
{
    public interface ISubsidiaryApplication : IApplication<SubsidiaryViewModel>
    {
        SubsidiaryDTO GetSubsidiaryByUser(UserDTO user);
        FileResult CreateAFD(AFDViewModel afdParams);

    }
}
