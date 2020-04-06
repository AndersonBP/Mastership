using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.DTO.Enums;
using Mastership.Domain.Interfaces;
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
        private readonly IEmailApplication _emailApplication;

        public PointTimeApplication(
            Lazy<IEmployeeApplication> employeeApplication,
            IPointTimeRepository repository,
            IMapper mapper, IUserDataService userDataService,
            IEmailApplication emailApplication
        ) : base(repository, mapper, userDataService)
        {
            this._emailApplication = emailApplication;
            this.employeeApplication = employeeApplication;
        }

        public ICollection<PointTimeViewModel> GetByDay(DateTime day, Guid employeId)
        {
            var registrations = this._repository.GetByDay(day, employeId)
                .OrderBy(x => x.DateTime)
                .ToList();
            return this.MapToViewModel(registrations);
        }

        public CheckRegistrationViewModel Register(CheckRegistrationViewModel vm, string domainName)
        {
            var employee = this.employeeApplication.Value.CheckRegistration(vm, domainName);
            employee.TrueAnswer = false;
            if (this.employeeApplication.Value.CheckAnswerQuestion(vm.QuestionType, employee.Id, vm.Answer))
            {
                var registration = this._repository.Save(
                    new PointTimeDTO {
                        DateTime = DateTime.Now,
                        EmployeeId = employee.Id,
                        Sequential = this.GetSequential(employee.Subsidiary.Id)
                    });

                employee.PointsTime.Add(this.MapToViewModel(registration));
                employee.PointsTime.OrderBy(x => x.DateTime);
                employee.TrueAnswer = true;
                if(!string.IsNullOrEmpty(employee.Email))
                    this._emailApplication.SendEmailAsync(registration);
            } else {
                employee.QuestionType = this.employeeApplication.Value.GetQuestionKey(vm.QuestionType);
            }
            
            return employee;
        }

        private long GetSequential(Guid subsidiaryId) {
            var currentSequential = this._repository.GetLastSequentialOf(subsidiaryId);
            return currentSequential + 1;
        }
    }
}
