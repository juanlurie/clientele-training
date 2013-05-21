using System.Data.Entity;
using AsbaBank.Core;
using AsbaBank.Infrastructure.DataStoreSelector;

namespace AsbaBank.Infrastructure.SqlInfrastructure
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private DbContext context;

        public EfUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            context.Dispose();
            context = ContextFactory.DataStore as DbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new EntityFrameworkRepository<TEntity>(context);
        }
    }
}