using AutoMapper;
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using System;

namespace Mastership.Application.Services
{
    public class EmployeeApplication : BaseApplication<EmployeeViewModel, EmployeeEntity, IEmployeeRepository>, IEmployeeApplication
    {
        private readonly IPointTimeApplication pointTimeApplication;
        public EmployeeApplication(IEmployeeRepository repository, IMapper mapper, IPointTimeApplication pointTimeApplication) : base(repository, mapper)
        {
            this.pointTimeApplication = pointTimeApplication;
        }

        public EmployeeViewModel CheckRegistration(EmployeeViewModel vm, string companyName)
        {
            var employe = this.MapToViewModel(this.Repository.GetByRegistration(vm.Registration));
            if (employe != null)
            {
                employe.PointsTime = this.pointTimeApplication.GetByDay(DateTime.Now.AbsoluteStart(), employe.Id);
            }
            return employe;
        }
    }
}
