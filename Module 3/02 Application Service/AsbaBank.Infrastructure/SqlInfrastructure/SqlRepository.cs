using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AsbaBank.Core;

namespace AsbaBank.Infrastructure.SqlInfrastructure
{
    sealed class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dataStore;
        private readonly PropertyInfo identityPropertyInfo;

        public SqlRepository(DbContext dataStore)
        {
            this.dataStore = dataStore;
            identityPropertyInfo = GetIdentityPropertyInformation();
        }

        internal int GetNextId()
        {
            //Database is auto id for now
            return 0;
        }

        private PropertyInfo GetIdentityPropertyInformation()
        {
            return typeof(TEntity)
                .GetProperties()
                .Single(propertyInfo => Attribute.IsDefined(propertyInfo, typeof(KeyAttribute)));
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return dataStore.Set<TEntity>().AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TEntity Get(object id)
        {
            return dataStore
                .Set<TEntity>()
                .AsQueryable()
                .SingleOrDefault(WithMatchingId(id));
        }

        public IList<TEntity> GetAll()
        {
            return dataStore
               .Set<TEntity>()
               .ToList();
        }

        private Func<TEntity, bool> WithMatchingId(object id)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression property = Expression.Property(parameter, identityPropertyInfo.Name);
            Expression target = Expression.Constant(id);
            Expression equalsMethod = Expression.Equal(property, target);
            Func<TEntity, bool> predicate = Expression.Lambda<Func<TEntity, bool>>(equalsMethod, parameter).Compile();

            return predicate;
        }

        public void Update(object id, TEntity item)
        {
            TEntity oldItem = Get(id);
            dataStore.Set<TEntity>().Remove(oldItem);
            dataStore.Set<TEntity>().Add(item);
        }

        public void Add(TEntity item)
        {
            dataStore.Set<TEntity>().Add(item);
            
        }

        public void Clear()
        {

        }

        public bool Contains(TEntity item)
        {
            return dataStore.Set<TEntity>()
                            .AsQueryable<TEntity>()
                            .Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public bool Remove(TEntity item)
        {
            dataStore.Set<TEntity>().Remove(item);
            return true;
        }

        public int Count
        {
            get { return dataStore.Set<TEntity>().Count(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}