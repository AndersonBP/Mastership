using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Mastership.Domain.DTO;
using AutoMapper;
using System.Linq;
using System;

namespace Mastership.Database.Repositories
{
    public class SubsidiaryRepository : BaseRepository<SubsidiaryDTO, SubsidiaryEntity>, ISubsidiaryRepository
    {
        public SubsidiaryRepository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

       

        public SubsidiaryDTO GetByUser(Guid id) {
            var query = this.Query().FirstOrDefault(x => x.UserId == id);
            return this._mapper.Map<SubsidiaryDTO>(query);
        }
    }
}
