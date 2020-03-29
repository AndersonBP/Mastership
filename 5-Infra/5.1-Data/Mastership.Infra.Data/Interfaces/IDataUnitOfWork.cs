using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mastership.Infra.Data.Interfaces
{
    public interface IDataUnitOfWork
    {
        IDataContext Context { get; }

        void StartTransaction();

        void StartTransaction(IsolationLevel level);

        void Commit();

        void Rollbak();

        void Clear();

    }
}
