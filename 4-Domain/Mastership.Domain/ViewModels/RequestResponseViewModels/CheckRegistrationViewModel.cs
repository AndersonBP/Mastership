using Mastership.Domain.DTO.Enums;

namespace Mastership.Domain.ViewModels
{
    public class CheckRegistrationViewModel: EmployeeViewModel
    {
        public KeyQuestionType QuestionType { get; set; }
        public string Answer { get; set; }
        public bool TrueAnswer { get; set; } = false;
    }
}
