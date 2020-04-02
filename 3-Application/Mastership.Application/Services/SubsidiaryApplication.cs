using AutoMapper;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class SubsidiaryApplication : BaseApplication<SubsidiaryViewModel, SubsidiaryDTO, ISubsidiaryRepository>, ISubsidiaryApplication
    {
        public SubsidiaryApplication(ISubsidiaryRepository repository, IMapper mapper) : base(repository, mapper) { }

        public SubsidiaryViewModel CheckDomainName(string domainName)
        {
            var subsidiary = this.MapToViewModel(this.Repository.GetByDomainName(domainName));
            if (subsidiary == null)
                throw new NotFoundException("Subsidiary not found!");

            return subsidiary;
        }
    }
}
