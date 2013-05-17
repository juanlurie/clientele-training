using System.Configuration;
using AsbaBank.Core;
using AsbaBank.Domain;
using AsbaBank.Infrastructure.InMemoryUnitOfWork;

namespace AsbaBank.Presentation.Shell
{
    public class DbContextFacade : AbsaBankContext
    {
        public IDataStore GetDataStore()
        {
            return new AbsaBankContext();
        }
    }

    public class InMemoryDataStoreFacade : InMemoryDataStore
    {
        public IDataStore GetDataStore()
        {
            return new InMemoryDataStore();
        }
    }

    public class DataStoreSelector
    {
        public IDataStore GetDataStoreFromAppConfiguration()
        {
            switch (ConfigurationManager.AppSettings["DataSource"].ToLower())
            {
                case "sql" : return new AbsaBankContext();
                case "inmemory" : return new InMemoryDataStore();
                default:return new InMemoryDataStore();
            }
        }
    }
}