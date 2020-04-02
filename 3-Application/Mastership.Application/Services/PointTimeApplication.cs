using AutoMapper;
using Mastership.Domain.DTO;
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
            this.Repository.Save(new PointTimeDTO() { Day = DateTime.Now.AbsoluteStart(), Hour = DateTime.Now.TimeOfDay, EmployeeId = Guid.Parse("efd11537-1f40-4b92-bf99-9370745dfc8d") });
        }
    }
}
