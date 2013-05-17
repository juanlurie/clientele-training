using System.Configuration;
using System.Data.Entity;
using AsbaBank.Core;
using AsbaBank.Domain.Models;

namespace AsbaBank.Domain
{
    public class AbsaBankContext : DbContext, IDataStore
    {
        public AbsaBankContext()
        {
            Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["AbsaBankContext"].ConnectionString;
            if (!Database.Exists())
                Database.Create();
        }

        DbSet<Client> Client { get; set; }
        DbSet<Address> Address { get; set; }
        public string DataStoreName { get { return "Sql Datastore"; } }
    }
}
