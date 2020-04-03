using Mastership.Domain.DTO.Enums;
using Mastership.Domain.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mastership.Domain.Interfaces.Application
{
    public interface IPointTimeApplication : IApplication<PointTimeViewModel> {
        ICollection<PointTimeViewModel> GetByDay(DateTime day, Guid employeId);
        KeyQuestionType GetQuestionKey(Nullable<KeyQuestionType> exclude = null);
        void Register();
    }
}
