using System.Data.Entity;
using AsbaBank.Core;

namespace AsbaBank.Infrastructure.SqlInfrastructure
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

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
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new EfRepository<TEntity>(context);
        }
    }
}