using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Repository;
using Mastership.Domain.ViewModels;
using Microsoft.AspNet.OData.Query;

namespace Mastership.Application.Services
{
    public abstract class BaseService<TVMType, TType, TRepository> 
        where TVMType : BaseViewModel, new()
        where TType : BaseEntity
        where TRepository : IRepository<TType>
    {
        protected readonly TRepository Repository;

        public BaseService(TRepository repository)
        {
            Repository = repository;
        }

        protected virtual void Validar(TType obj) { }

        protected virtual void Validar(TType[] objs)
        {
            foreach (var obj in objs)
                Validar(obj);
        }

        protected TType MapFromDTO(TVMType obj)
            => Mapper.Map<TType>(obj);

        protected IList<TType> MapFromDTO(IList<TVMType> obj)
            => obj.Select(x => Mapper.Map<TType>(x)).ToList();

        protected TVMType MapToDTO(TType obj)
            => Mapper.Map<TVMType>(obj);

        protected IList<TVMType> MapToDTO(IList<TType> obj)
            => obj.Select(x => Mapper.Map<TVMType>(x)).ToList();

        protected IList<TVMType> MapToDTO(IQueryable<TType> obj)
            => obj.ProjectTo<TVMType>().ToList();

        public virtual TVMType Adicionar(TType entity)
        {
            Validar(entity);

            var newEntity = Repository.Save(entity);

            return MapToDTO(newEntity);
        }

        public virtual TVMType[] Adicionar(TType[] entity)
        {
            Validar(entity);

            var newEntity = Repository.Save(entity);

            return newEntity.Select(x => MapToDTO(x)).ToArray();
        }

        public virtual TVMType Adicionar(TVMType obj)
        {
            var entity = MapFromDTO(obj);

            return Adicionar(entity);
        }

        public virtual TVMType[] Adicionar(TVMType[] obj)
        {
            var entityArray = obj.Select(x => MapFromDTO(x)).ToArray();

            return Adicionar(entityArray);
        }

        public virtual void AdicionarLote(TVMType[] obj)
        {
            var entityArray = obj.Select(x => MapFromDTO(x)).ToArray();

            Repository.InsertFast(entityArray);
        }

        public void Desabilitar(TVMType[] dtos)
        {
            foreach (var dto in dtos)
                Desabilitar(dto.Id);
        }

        public void Desabilitar(Guid id) => Repository.Disable(id);

        public virtual void Atualizar(TVMType[] lista)
        {
            foreach (var obj in lista)
                Atualizar(obj.Id, obj);
        }

        public virtual TVMType Atualizar(Guid id, TVMType obj)
        {
            if (id != obj.Id && obj.Id != Guid.Empty)
                throw new Exception("Id não é do objeto enviado");

            obj.Id = id;

            var entity = MapFromDTO(obj);

            Validar(entity);

            var newEntity = Repository.Save(entity);

            return MapToDTO(newEntity);
        }

        public TVMType Buscar(Guid id)
        {
            var entity = Repository.Get(id);

            return MapToDTO(entity);
        }

        public bool Existe(Guid id)
            => Repository.Exists(id);

        public IQueryable<TVMType> List()
            => Repository.List().ProjectTo<TVMType>();

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

            return updatedList.Select(x => MapToDTO(x));

        }

        public TVMType Upsert(TVMType obj)
        {
            PrepararReferencias(obj);

            var bdObject = BuscarEntidade(obj);
            if (bdObject == null)
                return Adicionar(obj);
            else
                return Atualizar(bdObject.Id, obj);
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
            var query = Repository.List().ProjectTo<TVMType>();
            var ignoreFlags = AllowedQueryOptions.Select | AllowedQueryOptions.Skip | AllowedQueryOptions.Top | AllowedQueryOptions.Expand | AllowedQueryOptions.OrderBy;
            var totalCount = (IQueryable<TVMType>)opts.ApplyTo(query, ignoreFlags);

            return totalCount.Count();
        }
    }
}
