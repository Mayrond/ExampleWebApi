using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using ExampleWebApi.Infrastructure;
using ExampleWebApi.Models;

namespace ExampleWebApi.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
#if DEBUG
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
#endif
        }

        protected override void Seed(Context dbContext)
        {
            AddUsers(dbContext);
            AddContractors(dbContext);
            AddCommertialProposal(dbContext);
        }

        private static void AddUsers(Context dbContext)
        {
            var admin = new UserGroup{Name = "Admin"};
            var manager = new UserGroup{Name = "Manager"};
            dbContext.UserGroups.AddOrUpdate(e => e.Name, admin);
            dbContext.UserGroups.AddOrUpdate(e => e.Name, manager);
            dbContext.SaveChanges();

            var users = new List<User>
            {
                new User{Name = "Vasya", Age = 10, Groups = new List<UserGroup>{manager}},
                new User{Name = "Alexandr", Age = 30, Groups = new List<UserGroup>{admin}},
            };
            for (int i = 0; i < 1000; i++)
                users.Add(new User { Name = "Petya"+i, Age = 20+i, Groups = new List<UserGroup> { manager, admin } });
            
            users.ForEach(u => dbContext.Users.AddOrUpdate(e => new{e.Name, e.Age}, u));
            dbContext.SaveChanges();
        }
        private static void AddContractors(Context dbContext)
        {
            var petya = dbContext.Users.First(u => u.Name == "Petya0");
            var vasya = dbContext.Users.First(u => u.Name == "Vasya");
            var alexandr = dbContext.Users.First(u => u.Name == "Alexandr");
            var contractors = new List<Contractor>
            {
                new Contractor{Name = "Contractor#1", Firm = new Firm{Name = "Firm#1"}, Curators = new List<User>{petya, vasya}},
                new Contractor{Name = "Contractor#2", Firm = new Firm{Name = "Firm#2"}, Curators = new List<User>{vasya, alexandr}},
                new Contractor{Name = "Contractor#3", Firm = new Firm{Name = "Firm#3"}, Curators = new List<User>{alexandr}},
            };
            contractors.ForEach(c => dbContext.Contractors.AddOrUpdate(e => e.Name, c));
            dbContext.SaveChanges();
        }
        private static void AddCommertialProposal(Context dbContext)
        {
            var contractor1 = dbContext.Contractors.First(c => c.Name == "Contractor#1");
            var contractor2 = dbContext.Contractors.First(c => c.Name == "Contractor#2");
            var contractor3 = dbContext.Contractors.First(c => c.Name == "Contractor#3");
            var commertialProposals = new List<CommertialProposal>
            {
                new CommertialProposal{Number = "CP1", From = contractor1, To = contractor2},
                new CommertialProposal{Number = "CP3", From = contractor1, To = contractor3}, 
            };
            for (int i = 0; i < 1000; i++)
                commertialProposals.Add(new CommertialProposal { Number = "CP2"+i, From = contractor2, To = contractor3});
            commertialProposals.ForEach(cp => dbContext.CommertialProposals.AddOrUpdate(e => e.Number, cp));
            dbContext.SaveChanges();
        }
    }
}
