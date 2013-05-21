using System.Configuration;
using AsbaBank.Core;

namespace AsbaBank.Infrastructure.DataStoreSelector
{
    public static class DataStoreSelector
    {
        private static IDataStore dataStore;

        static DataStoreSelector()
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
            AbstractDatastoreFactory factory2 = new DatastoreFactory();

            switch (ConfigurationManager.AppSettings["DataSource"].ToLower())
            {
                case "sql":
                    {
                        DataStore = factory2.AbsaBankContext(); break;
                    }
                case "inmemory":
                    {
                        DataStore = factory2.InMemoryDataStore(); break;
                    }
                default:
                    {
                        DataStore = factory2.InMemoryDataStore();
                        break;
                    }
            }
        }
    }
}