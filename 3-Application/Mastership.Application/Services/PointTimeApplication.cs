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
        private readonly IEmailApplication _emailApplication;

        public PointTimeApplication(
            Lazy<IEmployeeApplication> employeeApplication,
            IPointTimeRepository repository,
            IMapper mapper,
            IEmailApplication emailApplication
        ) : base(repository, mapper)
        {
            this._emailApplication = emailApplication;
            this.employeeApplication = employeeApplication;
        }

        public ICollection<PointTimeViewModel> GetByDay(DateTime day, Guid employeId)
        {
            var registrations = this.Repository.GetByDay(day, employeId)
                .OrderBy(x => x.DateTime)
                .ToList();
            return this.MapToViewModel(registrations);
        }

        public CheckRegistrationViewModel Register(CheckRegistrationViewModel vm, string domainName)
        {
            var checkRegistration = this.employeeApplication.Value.CheckRegistration(vm, domainName);
            checkRegistration.TrueAnswer = false;
            if (this.employeeApplication.Value.CheckAnswerQuestion(vm.QuestionType, checkRegistration.Id, vm.Answer))
            {
                var registration = this.Repository.Save(
                    new PointTimeDTO {
                        DateTime = DateTime.Now,
                        EmployeeId = checkRegistration.Id,
                        Sequential = this.GetSequential(checkRegistration.Subsidiary.Id)
                    });

                checkRegistration.PointsTime.Add(this.MapToViewModel(registration));
                checkRegistration.PointsTime.OrderBy(x => x.DateTime);
                checkRegistration.TrueAnswer = true;
                
                this._emailApplication.SendEmailAsync(registration);
            } else {
                checkRegistration.QuestionType = this.employeeApplication.Value.GetQuestionKey(vm.QuestionType);
            }
            
            return checkRegistration;
        }

        private long GetSequential(Guid subsidiaryId) {
            var currentSequential = this.Repository.GetLastSequentialOf(subsidiaryId);
            return currentSequential + 1;
        }
    }
}
