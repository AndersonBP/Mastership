using AutoMapper;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.DTO.Enums;
using Mastership.Domain.Exceptions;
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
    public class EmployeeApplication : BaseApplication<EmployeeViewModel, EmployeeDTO, IEmployeeRepository>, IEmployeeApplication
    {
        private readonly IPointTimeApplication pointTimeApplication;
        public EmployeeApplication(IEmployeeRepository repository, IMapper mapper, IUserDataService userDataService, IPointTimeApplication pointTimeApplication) : base(repository, mapper, userDataService)
        {
            this.pointTimeApplication = pointTimeApplication;
        }

        public override EmployeeViewModel Search(string id)
        {
            var gId = new Guid();
            if (Guid.TryParse(id, out gId))
            {
                return base.Search(gId);
            }
            else
                return this.MapToViewModel(this._repository.GetByForeingId(id));
        }

        public override EmployeeDTO Validar(EmployeeDTO obj)
        {
            var employe = string.IsNullOrEmpty(obj.ForeignId) ? this.Search(obj.Id) : this.Search(obj.ForeignId);
            if (employe!=null && !employe.SubsidiaryId.Equals(this._userDataService.SubsidiaryId))
                throw new ValidationException("Invalid operation!");

            obj.SubsidiaryId = this._userDataService.SubsidiaryId;
            if (employe != null)
            {
                obj.Id = employe.Id;
                obj.UserId = employe.UserId;
            }
            return obj;
        }

        public CheckRegistrationViewModel CheckRegistration(CheckRegistrationViewModel vm, string subName)
        {
            var employee = this._repository.GetByRegistrationAndDomainName(vm.Registration, subName);
            if (employee == null)
                throw new NotFoundException("Employee not found!");

            var registration = new CheckRegistrationViewModel()
            {
                FullName = employee.FullName,
                Name = employee.Name,
                Registration = employee.Registration,
                Email = employee.Email,
                PIS = employee.PIS,
                Subsidiary = this._mapper.Map<SubsidiaryViewModel>(employee.Subsidiary),
                PointsTime = this._mapper.Map<ICollection<PointTimeViewModel>>(employee.PointsTime.Where(x => x.DateTime.Date.Equals(DateTime.Now.AbsoluteStart())).OrderBy(x => x.DateTime)),
                Id = employee.Id
            };
            registration.Subsidiary.Employees = null;

            registration.QuestionType = this.GetQuestionKey();
            //registration.PointsTime = pointTimeApplication.GetByDay(DateTime.Now.AbsoluteStart(), employee.Id);
            return registration;
        }

        public KeyQuestionType GetQuestionKey(Nullable<KeyQuestionType> exclude = null)
        {
            var values = Enum.GetValues(typeof(KeyQuestionType)).Cast<KeyQuestionType>().Where(x => !exclude.HasValue || exclude.Value != x).ToArray();
            var rand = new Random();
            var questionKey = values.GetValue(rand.Next(values.Length));
            return (KeyQuestionType)questionKey;
        }

        public bool CheckAnswerQuestion(KeyQuestionType questionType, Guid employeeId, string answer)
        {
            var employee = this.Search(employeeId);
            switch (questionType)
            {
                case KeyQuestionType.BirthdayDay:
                    return employee.Birthday.Day.ToString().Equals(answer);
                case KeyQuestionType.AnniversaryMonth:
                    return employee.Birthday.Month.ToString().Equals(answer);
                case KeyQuestionType.AnniversaryYear:
                    return employee.Birthday.Year.ToString().Equals(answer);
                case KeyQuestionType.TwoLastCpf:
                    return employee.CPFNumbers.Substring(employee.CPFNumbers.Length - 2, 2).Equals(answer);
                case KeyQuestionType.ThreeFirstCpf:
                    return employee.CPFNumbers.Substring(0, 3).Equals(answer);
                case KeyQuestionType.FourFirstRG:
                    return employee.RG.Substring(0, 4).Equals(answer);
                case KeyQuestionType.AdmissionYear:
                    return employee.AdmissionDate.Year.ToString().Equals(answer);
                default:
                    return false;
            }
        }
    }
}
