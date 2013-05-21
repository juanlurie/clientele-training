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

        public static IDataStore Context
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
                        Context = datastoreFactory.AbsaBankContext(); break;
                    }
                case "inmemory":
                    {
                        Context = datastoreFactory.InMemoryDataStore(); break;
                    }
                default:
                    {
                        Context = datastoreFactory.InMemoryDataStore();
                        break;
                    }
            }
        }
    }
}