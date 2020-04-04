using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.DTO.Enums;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastership.Application.Services
{
    public class PointTimeApplication : BaseApplication<PointTimeViewModel, PointTimeDTO, IPointTimeRepository>, IPointTimeApplication
    {
        private readonly Lazy<IEmployeeApplication> employeeApplication;
        public PointTimeApplication(Lazy<IEmployeeApplication> employeeApplication, IPointTimeRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this.employeeApplication = employeeApplication;
        }

        public ICollection<PointTimeViewModel> GetByDay(DateTime day, Guid employeId)
        {
            return this.MapToViewModel(this.Repository.GetByDay(day, employeId).ToList());
        }



        public CheckRegistrationViewModel Register(CheckRegistrationViewModel vm, string domainName)
        {
            var employee = this.employeeApplication.Value.CheckRegistration(vm, domainName);
            employee.TrueAnswer = false;
            if (this.employeeApplication.Value.CheckAnswerQuestion(vm.QuestionType, employee.Id, vm.Answer))
            {
                employee.PointsTime.Add(this.MapToViewModel(this.Repository.Save(new PointTimeDTO() { DateTime = DateTime.Now, EmployeeId = employee.Id })));
                employee.PointsTime.OrderBy(x => x.DateTime);
                employee.TrueAnswer = true;
            }
            else
                employee.QuestionType = this.employeeApplication.Value.GetQuestionKey(vm.QuestionType);
            
            return employee;
        }
    }
}
