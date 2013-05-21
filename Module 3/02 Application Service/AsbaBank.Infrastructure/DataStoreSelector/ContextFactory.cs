using System.Configuration;
using AsbaBank.Core;

namespace AsbaBank.Infrastructure.DataStoreSelector
{
    public static class ContextFactory
    {
        private static IDataStore dataStore;

        static ContextFactory()
        {
            GetDataStoreFromAppConfiguration();
        }

        public static IDataStore DataStore
        {
            get
            {
                if(dataStore.IsDisposed)
                    GetDataStoreFromAppConfiguration();
                return dataStore;
            }
            set { dataStore = value; }
        }

        public static void GetDataStoreFromAppConfiguration()
        {
            AbstractDatastoreFactory datastoreFactory = new DatastoreFactory();

            switch (ConfigurationManager.AppSettings["DataSource"].ToLower())
            {
                case "sql":
                    {
                        DataStore = datastoreFactory.AbsaBankContext(); break;
                    }
                case "inmemory":
                    {
                        DataStore = datastoreFactory.InMemoryDataStore(); break;
                    }
                default:
                    {
                        DataStore = datastoreFactory.InMemoryDataStore();
                        break;
                    }
            }
        }
    }
}