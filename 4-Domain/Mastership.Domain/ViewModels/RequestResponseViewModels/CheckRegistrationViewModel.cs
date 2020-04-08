using Mastership.Domain.DTO.Enums;
using System;
using System.Collections.Generic;

namespace Mastership.Domain.ViewModels
{
    public class CheckRegistrationViewModel
    {
        public KeyQuestionType QuestionType { get; set; }
        public string Answer { get; set; }
        public bool TrueAnswer { get; set; } = false;

        public string Registration { get; set; }

        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PIS { get; set; }
        public string NSR { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Guid Id { get; set; }

        public virtual ICollection<PointTimeViewModel> PointsTime { get; set; }
        public virtual SubsidiaryViewModel Subsidiary { get; set; }
    }
}
