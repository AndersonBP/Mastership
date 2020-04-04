using Mastership.Domain.DTO.Enums;
using Mastership.Domain.ViewModels;
using System;

namespace Mastership.Domain.Interfaces.Application
{
    public interface IEmployeeApplication : IApplication<EmployeeViewModel> {
        CheckRegistrationViewModel CheckRegistration(CheckRegistrationViewModel vm, string companyName);
        KeyQuestionType GetQuestionKey(Nullable<KeyQuestionType> exclude = null);
        bool CheckAnswerQuestion(KeyQuestionType questionType, Guid employeeId, string answer);
    }
}
