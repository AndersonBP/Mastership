using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mastership.Domain.Entities;
using Mastership.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mastership.Infra.Data.Repositories
{
    public abstract class BaseRepository<TType> where TType : BaseEntity, new()
    {

        private DbSet<TType> _dbSet;

        protected IDataContext Context;

        protected BaseRepository(IDataUnitOfWork uow)
            => Context = uow.Context;

        protected DbSet<TType> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = Context.Set<TType>();

                return _dbSet;
            }
        }

        protected virtual IQueryable<TType> Query(bool tracking = false, bool withUserFilter = true)
            => (tracking ? DbSet : DbSet.AsNoTracking());

        public IQueryable<TType> List(bool withUserFilter = true)
            => Query(false, withUserFilter);

        public TType Get(Guid id)
            => Query(false).FirstOrDefault(x => x.Id == id);

        public virtual bool Exists(TType obj)
            => Exists(obj.Id);

        public virtual bool Exists(Guid id)
            => Query(false).Any(x => x.Id == id);

        public void Disable(Guid id)
        {
            var entidade = Get(id);
            entidade.Enable = false;

            Save(new TType[] { entidade }, true, true);
        }

        public void Delete(Guid id)
        {
            var toDelete = new TType { Id = id };
            DetachLocalObject(toDelete);
            Context.Entry(toDelete).State = EntityState.Deleted;
        }

        public void Delete(Guid[] ids)
        {
            foreach (var id in ids)
                Delete(id);

            Context.SaveChanges();
        }

        public virtual TType Save(TType obj)
            => Save(new TType[] { obj }, false, true).FirstOrDefault();

        public virtual TType[] Save(TType[] obj)
            => Save(obj, false, true);

        public TType[] InsertFast(TType[] lista)
        {

            var ids = lista.Select(x => x.Id).ToArray();

            foreach (var item in lista)
                item.CreationDate = DateTime.Now;

            DbSet.AddRange(lista);

            Context.SaveChanges();

            return lista;
        }

        protected TType Save(TType obj, bool changeState)
            => Save(new TType[] { obj }, false, changeState).FirstOrDefault();

        private TType[] Save(TType[] lista, bool disable, bool changeState)
        {

            foreach (var obj in lista)
            {

                if (changeState)
                    DetachLocalObject(obj);

                SetVirtualPropertiesUnchanged(obj);

                if (!Exists(obj))
                {
                    obj.Id = obj.Id != Guid.Empty ? obj.Id : Guid.NewGuid();
                    obj.CreationDate = DateTime.Now;
                    DbSet.Add(obj);
                }
                else
                {
                    if (changeState)
                        Context.Entry(obj).State = EntityState.Modified;

                    if (obj.Enable || !disable)
                    {
                        obj.ChangeDate = DateTime.Now;
                        Context.Entry(obj).Property(x => x.CreationDate).IsModified = false;
                    }

                    if (!obj.Enable && !disable)
                    {
                        Context.Entry(obj).Property(x => x.Enable).IsModified = false;
                    }
                }
            }

            Context.SaveChanges();

            return lista;
        }


        private void SetVirtualPropertiesUnchanged(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties().Where(p => p.GetMethod.IsVirtual).ToArray();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj, null);
                if (value == null)
                    continue;

                if (value.GetType().Name.Contains("List"))
                {
                    foreach (var val in (IList)value)
                        SetVirtualPropertiesUnchanged(val);
                }
                else
                {
                    DetachLocalObject(value);
                    try
                    {
                        Context.Entry(value).State = EntityState.Unchanged;
                    }
                    catch { }
                }
            }
        }

        protected void DetachLocalObject(object obj)
        {
            var local = DbSet.Local.FirstOrDefault(entry => entry.Id.Equals(((BaseEntity)obj).Id));
            if (local != null)
                Context.Entry(local).State = EntityState.Detached;
        }

        public virtual IEnumerable<TType> UpdateManyReturningObject(List<TType> objs)
        {
            var updatedList = new List<TType>();

            foreach (var obj in objs)
            {
                updatedList.Add(obj);
                Context.Entry(obj);
                Context.Set<TType>().Attach(obj).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return updatedList.Select(x => x);
        }
    }
}
