using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Infra.Data.Entities;
using Mastership.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mastership.Infra.Data.Repositories
{
    public abstract class BaseRepository<TDtoType, TEntity>
        where TDtoType : BaseDTO, new()
        where TEntity : BaseEntity, new()
    {
        protected readonly IMapper _mapper;
        private DbSet<TEntity> _dbSet;

        protected IDataContext Context;

        protected BaseRepository(IDataUnitOfWork uow, IMapper mapper)
        {
            this.Context = uow.Context;
            this._mapper = mapper;
        }


        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = Context.Set<TEntity>();

                return _dbSet;
            }
        }

        public virtual IQueryable<TEntity> Includes(IQueryable<TEntity> query)
        {
            return query;
        }

        protected virtual IQueryable<TEntity> Query(bool tracking = false, bool withUserFilter = true, bool includes = true)
        {
            var query = tracking ? DbSet: DbSet.AsNoTracking();
            return includes ? this.Includes(query):query;
        }

        public IQueryable<TDtoType> List(bool withUserFilter = true)
            => this._mapper.ProjectTo<TDtoType>(Query(false, withUserFilter));

        public TDtoType Get(Guid id)
            => this._mapper.Map<TDtoType>(Query(false, includes:false).FirstOrDefault(x => x.Id == id));

        public virtual bool Exists(TDtoType obj)
            => Exists(obj.Id);

        public virtual bool Exists(Guid id)
            => Query(false).Any(x => x.Id == id);

        public void Disable(Guid id)
        {
            var entidade = Get(id);
            entidade.Enable = false;

            Save(new TEntity[] { this._mapper.Map<TEntity>(entidade) }, true, true);
        }

        public void Delete(Guid id)
        {
            var toDelete = new TEntity { Id = id };
            DetachLocalObject(toDelete);
            Context.Entry(toDelete).State = EntityState.Deleted;
        }

        public void Delete(Guid[] ids)
        {
            foreach (var id in ids)
                Delete(id);

            Context.SaveChanges();
        }

        public virtual TDtoType Save(TDtoType obj)
            => Save(new TEntity[] { this._mapper.Map<TEntity>(obj) }, false, true).FirstOrDefault();

        public virtual TDtoType[] Save(TDtoType[] obj)
            => Save(this._mapper.Map<TEntity[]>(obj), false, true);

        public TDtoType[] InsertFast(TDtoType[] lista)
        {

            var ids = lista.Select(x => x.Id).ToArray();

            foreach (var item in lista)
                item.CreationDate = DateTime.Now;

            DbSet.AddRange(this._mapper.Map<TEntity[]>(lista));

            Context.SaveChanges();

            return lista;
        }

        protected TDtoType Save(TDtoType obj, bool changeState)
            => Save(new TEntity[] { this._mapper.Map<TEntity>(obj) }, false, changeState).FirstOrDefault();

        private TDtoType[] Save(TEntity[] lista, bool disable, bool changeState)
        {

            foreach (var obj in lista)
            {

                if (changeState)
                    DetachLocalObject(obj);

                SetVirtualPropertiesUnchanged(obj);

                if (!Exists(obj.Id))
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

            return this._mapper.Map<TDtoType[]>(lista);
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

        public virtual IEnumerable<TDtoType> UpdateManyReturningObject(List<TDtoType> objs)
        {
            var updatedList = new List<TEntity>();

            foreach (var obj in this._mapper.Map<List<TEntity>>(objs))
            {
                updatedList.Add(obj);
                Context.Entry(obj);
                Context.Set<TEntity>().Attach(obj).State = EntityState.Modified;
            }

            Context.SaveChanges();

            return this._mapper.Map<IEnumerable<TDtoType>>(updatedList.Select(x => x));
        }
    }
}
