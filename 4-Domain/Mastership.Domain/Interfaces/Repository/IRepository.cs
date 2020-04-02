using Mastership.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastership.Domain.Interfaces.Repository
{
    public interface IRepository<TDtoType> where TDtoType : BaseDTO
    {

        IQueryable<TDtoType> List(bool withUserFilter = true);

        TDtoType Get(Guid id);

        bool Exists(Guid obj);

        bool Exists(TDtoType obj);

        void Disable(Guid id);

        void Delete(Guid id);

        void Delete(Guid[] id);

        TDtoType Save(TDtoType obj);

        TDtoType[] Save(TDtoType[] obj);

        TDtoType[] InsertFast(TDtoType[] lista);

        IEnumerable<TDtoType> UpdateManyReturningObject(List<TDtoType> lista);

    }
}