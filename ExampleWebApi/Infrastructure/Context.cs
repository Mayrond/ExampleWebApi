using System.Data.Entity;
using ExampleWebApi.Models;

namespace ExampleWebApi.Infrastructure
{
    public class Context : DbContext
    {
        public Context()
            : base("MSSQL") 
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<CommertialProposal> CommertialProposals { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Firm> Firms { get; set; }
    }
}