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
        public PointTimeApplication(IPointTimeRepository repository, IMapper mapper) : base(repository, mapper) { }

        public ICollection<PointTimeViewModel> GetByDay(DateTime day, Guid employeId)
        {
            return  this.MapToViewModel(this.Repository.GetByDay(day, employeId).ToList());
        }

        public void Register()
        {
            this.Repository.Save(new PointTimeDTO() {DateTime = DateTime.Now, EmployeeId = Guid.Parse("546d31b0-f719-4789-b5f2-7ff94afa72e8") });
        }

        public KeyQuestionType GetQuestionKey(Nullable<KeyQuestionType> exclude = null)
        {
            var values = Enum.GetValues(typeof(KeyQuestionType)).Cast<KeyQuestionType>().Where(x=> !exclude.HasValue || exclude.Value!=x).ToArray();
            var rand = new Random();
            var questionKey = values.GetValue(rand.Next(values.Length));
            return (KeyQuestionType)questionKey;
        }
    }
}
