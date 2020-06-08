using CustomMediaTypeFormatterWebAPI_Demo.Models;
using System.Data.Entity;

namespace CustomMediaTypeFormatterWebAPI_Demo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("CustomerDB")
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}