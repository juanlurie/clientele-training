using AbsaBank.Infrastructure.EntityFramework.DataContext;
using AsbaBank.Infrastructure.InMemoryInfrastructure;

namespace AbsaBank.Infrastructure.EntityFramework.DataStoreSelector
{
    abstract class AbstractDatastoreFactory
    {
        public abstract AbsaBankContext AbsaBankContext();
        public abstract InMemoryDataStore InMemoryDataStore();
    }
}