using Mastership.Application.ViewModels;
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastership.Domain.Interfaces.Application
{
    public interface IApplication<TVMType> where TVMType : BaseViewModel
    {
        int Count(ODataQueryOptions<TVMType> opts);

        IQueryable<TVMType> List();

        TVMType Add(TVMType obj);

        TVMType[] Add(TVMType[] obj);

        void Disable(Guid id);

        void Disable(TVMType[] obj);

        TVMType Update(Guid id, TVMType obj);

        TVMType Search(Guid id);

        TVMType Upsert(TVMType obj);

        IList<TVMType> Upsert(IList<TVMType> objList);

        IEnumerable<TVMType> UpdateMany(List<TVMType> objs);
    }
}
