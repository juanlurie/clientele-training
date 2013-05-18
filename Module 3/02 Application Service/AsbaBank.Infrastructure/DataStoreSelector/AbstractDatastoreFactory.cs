using AsbaBank.Infrastructure.DataContext;
using AsbaBank.Infrastructure.InMemoryInfrastructure;

namespace AsbaBank.Infrastructure.DataStoreSelector
{
    abstract class AbstractDatastoreFactory
    {
        public abstract AbsaBankContext AbsaBankContext();
        public abstract InMemoryDataStore InMemoryDataStore();
    }
}