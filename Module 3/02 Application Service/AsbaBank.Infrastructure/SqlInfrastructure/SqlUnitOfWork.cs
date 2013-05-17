using System.Data.Entity;
using AsbaBank.Core;

namespace AsbaBank.Infrastructure.SqlInfrastructure
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public SqlUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            //??if save fails then all changes reverted anyway?
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new SqlRepository<TEntity>(context);
        }
    }
}