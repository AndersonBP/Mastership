using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.DTO.Enums;
using Mastership.Domain.Interfaces;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastership.Application.Services
{
    public class PointTimeApplication : BaseApplication<PointTimeViewModel, PointTimeDTO, IPointTimeRepository>, IPointTimeApplication
    {
        private readonly Lazy<IEmployeeApplication> employeeApplication;
        private readonly IEmailApplication _emailApplication;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PointTimeApplication(
            Lazy<IEmployeeApplication> employeeApplication,
            IPointTimeRepository repository,
            IMapper mapper, IUserDataService userDataService,
            IEmailApplication emailApplication,
            IHttpContextAccessor httpContextAccessor
        ) : base(repository, mapper, userDataService)
        {
            this._emailApplication = emailApplication;
            this.employeeApplication = employeeApplication;
            this._httpContextAccessor = httpContextAccessor;
        }

        public ICollection<PointTimeViewModel> GetByDay(DateTime day, Guid employeId)
        {
            var registrations = this._repository.GetByDay(day, employeId)
                .OrderBy(x => x.DateTime)
                .ToList();
            return this.MapToViewModel(registrations);
        }

        public IEnumerable<PointTimeDTO> GetByRange(DateTime start, DateTime end, Guid subsidiary)
        {
            return this._repository.GetByRange(start, end, subsidiary);
        }

        public CheckRegistrationViewModel Register(CheckRegistrationViewModel vm, string domainName)
        {
            var employeeClock = this.employeeApplication.Value.CheckRegistration(vm, domainName);
            employeeClock.TrueAnswer = false;
            if (this.employeeApplication.Value.CheckAnswerQuestion(vm.QuestionType, employeeClock.Id, vm.Answer))
            {
                var registration = this._repository.Save(
                    new PointTimeDTO
                    {
                        DateTime = DateTime.Now,
                        EmployeeId = employeeClock.Id,
                        Latitude = vm.Latitude,
                        Longitude = vm.Longitude,
                        IP = this._httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                        Sequential = this.GetSequential(employeeClock.Subsidiary.Id)
                    });

                employeeClock.PointsTime.Add(this.MapToViewModel(registration));
                employeeClock.PointsTime.OrderBy(x => x.DateTime);
                employeeClock.TrueAnswer = true;
                employeeClock.NSR = registration.Sequential.ToString().PadLeft(9, '0');
                if (!string.IsNullOrEmpty(employeeClock.Email))
                    this._emailApplication.SendEmailAsync(registration, employeeClock);
            }
            else
            {
                employeeClock.QuestionType = this.employeeApplication.Value.GetQuestionKey(vm.QuestionType);
            }

            return employeeClock;
        }

        private long GetSequential(Guid subsidiaryId)
        {
            var currentSequential = this._repository.GetLastSequentialOf(subsidiaryId);
            return currentSequential + 1;
        }
    }
}
