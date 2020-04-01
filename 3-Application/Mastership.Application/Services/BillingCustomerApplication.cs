using AutoMapper;
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class BillingCustomerApplication : BaseApplication<BillingCustomerViewModel, BillingCustomerEntity, IBillingCustomerRepository>, IBillingCustomerApplication
    {
        public BillingCustomerApplication(IBillingCustomerRepository repository, IMapper mapper) : base(repository, mapper) { }

    }
}
