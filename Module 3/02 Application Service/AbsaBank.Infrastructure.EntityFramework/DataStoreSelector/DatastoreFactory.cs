using AbsaBank.Infrastructure.EntityFramework.DataContext;
using AsbaBank.Infrastructure.DataStoreSelector;
using AsbaBank.Infrastructure.InMemoryInfrastructure;

namespace AbsaBank.Infrastructure.EntityFramework.DataStoreSelector
{
    class DatastoreFactory : AbstractDatastoreFactory
    {
        public override AbsaBankContext AbsaBankContext()
        {
            return new AbsaBankContext();
        }

        public override InMemoryDataStore InMemoryDataStore()
        {
            return new InMemoryDataStore();
        }
    }
}