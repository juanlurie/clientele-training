using System.Configuration;
using System.Data.Entity;
using AsbaBank.Core;
using AsbaBank.Domain.Models;

namespace AsbaBank.Infrastructure.DataContext
{
    public class AbsaBankContext : DbContext, IDataStore
    {
        public AbsaBankContext()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["AbsaBankContext"].ConnectionString;
            if (!Database.Exists())
                Database.Create();
        }

        // ReSharper disable UnusedMember.Local
        DbSet<Client> Client { get; set; }
        DbSet<Address> Address { get; set; }
        // ReSharper restore UnusedMember.Local

        public string DataStoreName { get { return "Sql Datastore"; } }
    }
}
