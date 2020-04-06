using AutoMapper;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces;

namespace Mastership.Application.Services
{
    public class BillingCustomerApplication : BaseApplication<BillingCustomerViewModel, BillingCustomerDTO, IBillingCustomerRepository>, IBillingCustomerApplication
    {
        public BillingCustomerApplication(IBillingCustomerRepository repository, IMapper mapper, IUserDataService userDataService) : base(repository, mapper, userDataService) { }

    }
}
