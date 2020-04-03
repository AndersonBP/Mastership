using AutoMapper;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using System;

namespace Mastership.Application.Services
{
    public class EmployeeApplication : BaseApplication<EmployeeViewModel, EmployeeDTO, IEmployeeRepository>, IEmployeeApplication
    {
        private readonly IPointTimeApplication pointTimeApplication;
        public EmployeeApplication(IEmployeeRepository repository, IMapper mapper, IPointTimeApplication pointTimeApplication) : base(repository, mapper)
        {
            this.pointTimeApplication = pointTimeApplication;
        }

        public CheckRegistrationViewModel CheckRegistration(CheckRegistrationViewModel vm, string subName)
        {
            var employe = this._mapper.Map<CheckRegistrationViewModel>(this.MapToViewModel(this.Repository.GetByRegistrationAndDomainName(vm.Registration, subName)));
            if (employe == null)
                throw new NotFoundException("Employee not found!");

            employe.QuestionType = this.pointTimeApplication.GetQuestionKey();
            employe.PointsTime =  pointTimeApplication.GetByDay(DateTime.Now.AbsoluteStart(), employe.Id);
            return employe;
        }
    }
}
