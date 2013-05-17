using System.Data.Entity;
using AsbaBank.Core;
using AsbaBank.Infrastructure.InMemoryUnitOfWork;
using AsbaBank.Infrastructure.SqlInfrastructure;

namespace AsbaBank.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            unitOfWork = new SqlUnitOfWork(context);
        }

        public UnitOfWork(InMemoryDataStore context)
        {
            unitOfWork = new InMemoryUnitOfWork.InMemoryUnitOfWork(context);
        }

        private readonly IUnitOfWork unitOfWork;
        public void Commit()
        {
            unitOfWork.Commit();
        }

        public void Rollback()
        {
            unitOfWork.Rollback();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return unitOfWork.GetRepository<TEntity>();
        }
    }
}
