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
    internal sealed class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        private readonly PropertyInfo identityPropertyInfo;

        public EfRepository(DbContext dataStore)
        {
            dbSet = dataStore.Set<TEntity>();
            identityPropertyInfo = GetIdentityPropertyInformation();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return dbSet.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TEntity Get(object id)
        {
            return dbSet
                .AsQueryable()
                .SingleOrDefault(WithMatchingId(id));
        }

        public IList<TEntity> GetAll()
        {
            return dbSet
                .ToList();
        }

        public void Update(object id, TEntity item)
        {
        }

        public void Add(TEntity item)
        {
            dbSet.Add(item);
        }

        public void Clear()
        {
        }

        public bool Contains(TEntity item)
        {
            return dbSet.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
        }

        public bool Remove(TEntity item)
        {
            dbSet.Remove(item);
            return true;
        }

        public int Count
        {
            get { return dbSet.Count(); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        private PropertyInfo GetIdentityPropertyInformation()
        {
            return typeof (TEntity)
                .GetProperties()
                .Single(propertyInfo => Attribute.IsDefined(propertyInfo, typeof (KeyAttribute)));
        }

        private Func<TEntity, bool> WithMatchingId(object id)
        {
            ParameterExpression parameter = Expression.Parameter(typeof (TEntity), "x");
            Expression property = Expression.Property(parameter, identityPropertyInfo.Name);
            Expression target = Expression.Constant(id);
            Expression equalsMethod = Expression.Equal(property, target);
            Func<TEntity, bool> predicate = Expression.Lambda<Func<TEntity, bool>>(equalsMethod, parameter).Compile();

            return predicate;
        }
    }
}