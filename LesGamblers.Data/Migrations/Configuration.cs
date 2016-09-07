namespace LesGamblers.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    using LesGamblers.Models;

    public sealed class Configuration : DbMigrationsConfiguration<LesGamblers.Data.LesGamblersDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LesGamblers.Data.LesGamblersDbContext context)
        {
            SeedRolesAndAdmin(context);
        }

        private void SeedRolesAndAdmin(LesGamblers.Data.LesGamblersDbContext context)
        {
            if (context.Users.Count() > 0)
            {
                return;
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<Gambler>(new UserStore<Gambler>(context));

            userManager.PasswordValidator = new MinimumLengthValidator(5);

            if (!roleManager.RoleExists(LesGamblers.Common.GlobalConstants.AdministratorRoleName))
            {
                roleManager.Create(new IdentityRole(LesGamblers.Common.GlobalConstants.AdministratorRoleName));
            }

            var admin1 = new Gambler
            {
                UserName = "martoadmin@abv.bg",
                Email = "martoadmin@abv.bg",
                PhoneNumber = "0888888888",
                FirstName = "Martin",
                LastName = "Atanasov"
            };

            if (userManager.FindByName("martoadmin@abv.bg") == null)
            {
                var result = userManager.Create(admin1, "martoCheater123");
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin1.Id, LesGamblers.Common.GlobalConstants.AdministratorRoleName);
                }
            }

            context.SaveChanges();
        }
    }
}
