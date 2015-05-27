namespace Ankk.Data.Migrations
{
    using Ankk.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Ankk.Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<AnkkDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AnkkDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            this.CreateAdmin(context);
        }

        private void CreateAdmin(AnkkDbContext context)
        {
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AnkkDbContext()));
            //var roleCreate = roleManager.Create(new IdentityRole("Administrator"));
            //if (!roleCreate.Succeeded)
            //{
            //    throw new ArgumentNullException("The role wasn't create " + roleCreate.Errors);
            //}

            //// 2 step -> create admin 
            //var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = new User()
            //{
            //    UserName = "admin@admin.com",
            //    Email = "admin@admin.com"
            //};
            //var createUserResult = userManager.Create(user, "123456");
            //if (!createUserResult.Succeeded)
            //{
            //    throw new ArgumentNullException("The role wasn't create " + createUserResult.Errors);
            //}

            //// 3 step -> 
            //userManager.AddToRole(user.Id, "Administrator");

           
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            var user = new User
            {
                UserName = "ankk@ankk.bg",
                Email = "ankk@ankk.bg",
            };

            if (!(context.Users.Any(u => u.Email == "ankk@ankk.bg")))
            {
                var password = "123456";
                var userCreateResult = userManager.Create(user, password);

                // Create "Administrator" role
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var roleCreateResult = roleManager.Create(new IdentityRole("Administrator"));
                if (!roleCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleCreateResult.Errors));
                }

                // Add "admin" user to "Administrator" role
                var addAdminRoleResult = userManager.AddToRole(user.Id, "Administrator");
                if (!addAdminRoleResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
                }

                context.SaveChanges();
            }
        }
    }
};
