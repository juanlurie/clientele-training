using AsbaBank.Infrastructure.DataContext;
using AsbaBank.Infrastructure.InMemoryInfrastructure;

namespace AsbaBank.Infrastructure.DataStoreSelector
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