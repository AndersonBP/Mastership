using AutoMapper;
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class PointTimeApplication : BaseApplication<PointTimeViewModel, PointTimeEntity, IPointTimeRepository>, IPointTimeApplication
    {
        public PointTimeApplication(IPointTimeRepository repository, IMapper mapper) : base(repository, mapper) { }

    }
}
