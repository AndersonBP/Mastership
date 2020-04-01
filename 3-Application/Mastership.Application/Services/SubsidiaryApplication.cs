using AutoMapper;
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class SubsidiaryApplication : BaseApplication<SubsidiaryViewModel, SubsidiaryEntity, ISubsidiaryRepository>, ISubsidiaryApplication
    {
        public SubsidiaryApplication(ISubsidiaryRepository repository, IMapper mapper) : base(repository, mapper) { }

    }
}
