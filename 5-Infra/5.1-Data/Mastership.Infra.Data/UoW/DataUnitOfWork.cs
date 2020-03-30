using Mastership.Infra.Data.Context;
using Mastership.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Mastership.Infra.Data.UoW
{
    public class DataUnitOfWork : IDataUnitOfWork
    {

        private readonly DataContext DataContext;

        private IDbContextTransaction Transaction;

        IDataContext IDataUnitOfWork.Context
            => DataContext;

        public DataUnitOfWork(DataContext context)
            => DataContext = context;

        public void StartTransaction()
            => Transaction = DataContext.Database.BeginTransaction();

        public void StartTransaction(IsolationLevel level)
            => Transaction = DataContext.Database.BeginTransaction(level);

        public void Commit()
        {
            if (Transaction != null)
                Transaction.Commit();
        }

        public void Rollbak()
        {
            if (Transaction != null)
                Transaction.Rollback();
        }

        public void Clear()
            => DataContext.Clear();
    }
}
