using AutoMapper;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.DTO.Enums;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using System;
using System.Linq;

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

            employe.QuestionType = this.GetQuestionKey();
            employe.PointsTime = pointTimeApplication.GetByDay(DateTime.Now.AbsoluteStart(), employe.Id);
            return employe;
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
                    return employee.Birthday.Day.Equals(answer);
                case KeyQuestionType.AnniversaryMonth:
                    return employee.Birthday.Month.Equals(answer);
                case KeyQuestionType.AnniversaryYear:
                    return employee.Birthday.Year.Equals(answer);
                case KeyQuestionType.TwoLastCpf:
                    return employee.CPFNumbers.Substring(employee.CPFNumbers.Length - 2, 2).Equals(answer);
                case KeyQuestionType.ThreeFirstCpf:
                    return employee.CPFNumbers.Substring(0, 2).Equals(answer);
                default:
                    return false;
            }
        }
    }
}
