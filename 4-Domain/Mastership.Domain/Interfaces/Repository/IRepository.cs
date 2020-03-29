using Mastership.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastership.Domain.Interfaces.Repository
{
    public interface IRepository<TType> where TType : BaseEntity
    {

        IQueryable<TType> List(bool withUserFilter = true);

        TType Get(Guid id);

        bool Exists(Guid obj);

        bool Exists(TType obj);

        void Disable(Guid id);

        void Delete(Guid id);

        void Delete(Guid[] id);

        TType Save(TType obj);

        TType[] Save(TType[] obj);

        TType[] InsertFast(TType[] lista);

        IEnumerable<TType> UpdateManyReturningObject(List<TType> lista);

    }
}