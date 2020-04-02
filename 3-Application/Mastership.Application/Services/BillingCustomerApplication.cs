using AutoMapper;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Domain.DTO;

namespace Mastership.Application.Services
{
    public class BillingCustomerApplication : BaseApplication<BillingCustomerViewModel, BillingCustomerDTO, IBillingCustomerRepository>, IBillingCustomerApplication
    {
        public BillingCustomerApplication(IBillingCustomerRepository repository, IMapper mapper) : base(repository, mapper) { }

    }
}
