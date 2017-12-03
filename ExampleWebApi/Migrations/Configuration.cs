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

        protected override void Seed(Context context)
        {
            AddUsers(context);
            AddContractors(context);
            AddCommertialProposal(context);
        }

        private static void AddUsers(Context context)
        {
            var admin = new UserGroup{Name = "Admin"};
            var manager = new UserGroup{Name = "Manager"};
            context.UserGroups.AddOrUpdate(e => e.Name, admin);
            context.UserGroups.AddOrUpdate(e => e.Name, manager);
            context.SaveChanges();

            var users = new List<User>
            {
                new User{Name = "Vasya", Age = 10, Groups = new List<UserGroup>{manager}},
                new User{Name = "Petya", Age = 20, Groups = new List<UserGroup>{manager, admin}},
                new User{Name = "Alexandr", Age = 30, Groups = new List<UserGroup>{admin}},
            };
            users.ForEach(u => context.Users.AddOrUpdate(e => new{e.Name, e.Age}, u));
            context.SaveChanges();
        }
        private static void AddContractors(Context context)
        {
            var petya = context.Users.First(u => u.Name == "Petya");
            var vasya = context.Users.First(u => u.Name == "Vasya");
            var alexandr = context.Users.First(u => u.Name == "Alexandr");
            var contractors = new List<Contractor>
            {
                new Contractor{Name = "Contractor#1", Firm = new Firm{Name = "Firm#1"}, Curators = new List<User>{petya, vasya}},
                new Contractor{Name = "Contractor#2", Firm = new Firm{Name = "Firm#2"}, Curators = new List<User>{vasya, alexandr}},
                new Contractor{Name = "Contractor#3", Firm = new Firm{Name = "Firm#3"}, Curators = new List<User>{alexandr}},
            };
            contractors.ForEach(c => context.Contractors.AddOrUpdate(e => e.Name, c));
            context.SaveChanges();
        }
        private static void AddCommertialProposal(Context context)
        {
            var contractor1 = context.Contractors.First(c => c.Name == "Contractor#1");
            var contractor2 = context.Contractors.First(c => c.Name == "Contractor#2");
            var contractor3 = context.Contractors.First(c => c.Name == "Contractor#3");
            var commertialProposals = new List<CommertialProposal>
            {
                new CommertialProposal{Number = "CP1", From = contractor1, To = contractor2},
                new CommertialProposal{Number = "CP2", From = contractor2, To = contractor3},
                new CommertialProposal{Number = "CP3", From = contractor1, To = contractor3}, 
            };
            commertialProposals.ForEach(cp => context.CommertialProposals.AddOrUpdate(e => e.Number, cp));
            context.SaveChanges();
        }
    }
}
