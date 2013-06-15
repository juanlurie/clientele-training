using System.Data.Entity;
using AbsaBank.Infrastructure.EntityFramework.SqlInfrastructure;
using AsbaBank.Core;
using AsbaBank.Infrastructure.InMemoryInfrastructure;

namespace AbsaBank.Infrastructure.Common.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWork(DbContext context)
        {
            unitOfWork = new EfUnitOfWork(context);
        }

        public UnitOfWork(InMemoryDataStore context)
        {
            unitOfWork = new InMemoryUnitOfWork(context);
        }

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