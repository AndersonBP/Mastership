using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Mastership.Domain.Interfaces.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Domain.DTO;

namespace Mastership.Application.Services
{
    public abstract class BaseApplication<TVMType, TType, TRepository>
        where TVMType : BaseViewModel, new()
        where TType : BaseDTO
        where TRepository : IRepository<TType>
    {
        protected readonly TRepository Repository;
        private readonly IMapper _mapper;

        public BaseApplication(TRepository repository, IMapper mapper)
        {
            Repository = repository;
            this._mapper = mapper;
        }

        protected virtual void Validar(TType obj) { }

        protected virtual void Validar(TType[] objs)
        {
            foreach (var obj in objs)
                Validar(obj);
        }

        protected TType MapFromDTO(TVMType obj)
            => _mapper.Map<TType>(obj);

        protected IList<TType> MapFromDTO(IList<TVMType> obj)
            => obj.Select(x => _mapper.Map<TType>(x)).ToList();

        protected TVMType MapToViewModel(TType obj)
            => _mapper.Map<TVMType>(obj);

        protected IList<TVMType> MapToViewModel(IList<TType> list)
            => list.Select(x => _mapper.Map<TVMType>(x)).ToList();

        protected IList<TVMType> MapToDTO(IQueryable<TType> list)
                       => list.Select(x => _mapper.Map<TVMType>(x)).ToList();


        public virtual TVMType Add(TType entity)
        {
            Validar(entity);

            var newEntity = Repository.Save(entity);

            return MapToViewModel(newEntity);
        }

        public virtual TVMType[] Add(TType[] entity)
        {
            Validar(entity);

            var newEntity = Repository.Save(entity);

            return newEntity.Select(x => MapToViewModel(x)).ToArray();
        }

        public virtual TVMType Add(TVMType obj)
        {
            var entity = MapFromDTO(obj);

            return Add(entity);
        }

        public virtual TVMType[] Add(TVMType[] obj)
        {
            var entityArray = obj.Select(x => MapFromDTO(x)).ToArray();

            return Add(entityArray);
        }

        public virtual void AddRange(TVMType[] obj)
        {
            var entityArray = obj.Select(x => MapFromDTO(x)).ToArray();

            Repository.InsertFast(entityArray);
        }

        public void Disable(TVMType[] dtos)
        {
            foreach (var dto in dtos)
                Disable(dto.Id);
        }

        public void Disable(Guid id) => Repository.Disable(id);

        public virtual void Update(TVMType[] lista)
        {
            foreach (var obj in lista)
                Update(obj.Id, obj);
        }

        public virtual TVMType Update(Guid id, TVMType obj)
        {
            if (id != obj.Id && obj.Id != Guid.Empty)
                throw new Exception("Id não é do objeto enviado");

            obj.Id = id;

            var entity = MapFromDTO(obj);

            Validar(entity);

            var newEntity = Repository.Save(entity);

            return MapToViewModel(newEntity);
        }

        public TVMType Search(Guid id)
        {
            var entity = Repository.Get(id);

            return MapToViewModel(entity);
        }

        public bool Existe(Guid id)
            => Repository.Exists(id);

        public IQueryable<TVMType> List()
            => Repository.List().Select(x => MapToViewModel(x));

        protected virtual void PrepararReferencias(TVMType obj) { }

        protected virtual TType BuscarEntidade(TVMType obj)
            => Repository.Get(obj.Id);

        public IEnumerable<TVMType> UpdateMany(List<TVMType> objs)
        {
            var listEntity = new List<TType>();


            foreach (var obj in objs)
            {
                listEntity.Add(BuscarEntidade(obj));
            }

            var updatedList = Repository.UpdateManyReturningObject(listEntity);

            return updatedList.Select(x => MapToViewModel(x));

        }

        public TVMType Upsert(TVMType obj)
        {
            PrepararReferencias(obj);

            var bdObject = BuscarEntidade(obj);
            if (bdObject == null)
                return Add(obj);
            else
                return Update(bdObject.Id, obj);
        }

        public IList<TVMType> Upsert(IList<TVMType> listObj)
        {
            var newList = new List<TVMType>();

            foreach (var obj in listObj)
                newList.Add(Upsert(obj));

            return newList;
        }

        public int Count(ODataQueryOptions<TVMType> opts)
        {
            var query = Repository.List();
            var ignoreFlags = AllowedQueryOptions.Select | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.Expand | AllowedQueryOptions.OrderBy;
            var totalCount = (IQueryable<TVMType>)opts.ApplyTo(query, ignoreFlags);

            return totalCount.Count();
        }
    }
}
