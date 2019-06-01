namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Models.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repository.Models.BlogContext context)
        {
            SeedRoles(context);
            SeedUsers(context);



        }

        private void SeedPosts(BlogContext context)
        {

        }

        private void SeedUsers(BlogContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            if (!context.Users.Any(x => x.UserName == "Admin"))
            {
                var user = new User { UserName = "Admin" };
                var adminResult = manager.Create(user, "1234");
                if (adminResult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }
            if (!context.Users.Any(x => x.UserName == "Asia"))
            {
                var user = new User { UserName = "asia@AspNetMvc.pl" };
                var adminResult = manager.Create(user, "_1234Abc");
                if (adminResult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                }
            }
            if (!context.Users.Any(x => x.UserName == "Bloger1"))
            {
                var user = new User { UserName = "Bloger1@AspNetMvc.pl" };
                var adminResult = manager.Create(user, "_1234Abc");
                if (adminResult.Succeeded)
                {
                    manager.AddToRole(user.Id, "Bloger");
                }
            }
        }

        private void SeedRoles(BlogContext context)
        {
            var RoleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());

            if (!RoleManager.RoleExists("Admin")) //jrzeli w role managerze nie istnieje admin to go stworz
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                RoleManager.Create(role);

            }
            if (!RoleManager.RoleExists("Bloger"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Bloger";
                RoleManager.Create(role);
            }
        }
    }
}

