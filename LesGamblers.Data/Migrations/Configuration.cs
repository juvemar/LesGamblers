namespace LesGamblers.Data.Migrations
{
    using System;
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
            SeedRoles(context);
            SeedUsers(context);
            SeedGames(context);
        }

        private void SeedRoles(LesGamblers.Data.LesGamblersDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<Gambler>(new UserStore<Gambler>(context));

            userManager.PasswordValidator = new MinimumLengthValidator(5);

            if (!roleManager.RoleExists(LesGamblers.Common.GlobalConstants.AdministratorRoleName))
            {
                roleManager.Create(new IdentityRole(LesGamblers.Common.GlobalConstants.AdministratorRoleName));
            }

            var admin1 = new Gambler
            {
                UserName = "cheatGambler@gamb.bg",
                Email = "cheatGambler@gamb.bg",
                PhoneNumber = "0888888888",
                FirstName = "Martin",
                LastName = "Atanasov"
            };

            if (userManager.FindByName("cheatGambler@gamb.bg") == null)
            {
                var result = userManager.Create(admin1, "martoCheater123");
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin1.Id, LesGamblers.Common.GlobalConstants.AdministratorRoleName);
                }
            }

            var admin2 = new Gambler
            {
                UserName = "TeodorTodorov@gamb.bg",
                Email = "TeodorTodorov@gamb.bg",
                PhoneNumber = "0888888888",
                FirstName = "Teodor",
                LastName = "Todorov"
            };

            if (userManager.FindByName("TeodorTodorov@gamb.bg") == null)
            {
                var result = userManager.Create(admin2, "teoCheater123");
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin2.Id, LesGamblers.Common.GlobalConstants.AdministratorRoleName);
                }
            }

            context.SaveChanges();
        }

        private void SeedGames(LesGamblers.Data.LesGamblersDbContext context)
        {
            if (context.Games.Count() != 0)
            {
                return;
            }

            context.Configuration.LazyLoadingEnabled = true;

            var game1 = new Game
            {
                HostTeam = CountryType.France.ToString(),
                GuestTeam = CountryType.Romania.ToString(),
                Date = new DateTime(2016, 6, 10, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game1);

            var game2 = new Game
            {
                HostTeam = CountryType.Albania.ToString(),
                GuestTeam = CountryType.Switzerland.ToString(),
                Date = new DateTime(2016, 6, 11, 16, 00, 00)
            };
            context.Games.AddOrUpdate(game2);

            var game3 = new Game
            {
                HostTeam = CountryType.Wales.ToString(),
                GuestTeam = CountryType.Slovakia.ToString(),
                Date = new DateTime(2016, 6, 11, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game3);

            var game4 = new Game
            {
                HostTeam = CountryType.England.ToString(),
                GuestTeam = CountryType.Russia.ToString(),
                Date = new DateTime(2016, 6, 11, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game4);

            var game5 = new Game
            {
                HostTeam = CountryType.Poland.ToString(),
                GuestTeam = CountryType.Northern_Ireland.ToString(),
                Date = new DateTime(2016, 6, 12, 16, 00, 00)
            };
            context.Games.AddOrUpdate(game5);

            var game6 = new Game
            {
                HostTeam = CountryType.Germany.ToString(),
                GuestTeam = CountryType.Ukraine.ToString(),
                Date = new DateTime(2016, 6, 12, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game6);

            var game7 = new Game
            {
                HostTeam = CountryType.Turkey.ToString(),
                GuestTeam = CountryType.Croatia.ToString(),
                Date = new DateTime(2016, 6, 12, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game7);

            var game8 = new Game
            {
                HostTeam = CountryType.Spain.ToString(),
                GuestTeam = CountryType.Czech_Republic.ToString(),
                Date = new DateTime(2016, 6, 13, 16, 00, 00)
            };
            context.Games.AddOrUpdate(game8);

            var game9 = new Game
            {
                HostTeam = CountryType.Republic_of_Ireland.ToString(),
                GuestTeam = CountryType.Sweden.ToString(),
                Date = new DateTime(2016, 6, 13, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game9);

            var game10 = new Game
            {
                HostTeam = CountryType.Belgium.ToString(),
                GuestTeam = CountryType.Italy.ToString(),
                Date = new DateTime(2016, 6, 13, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game10);

            var game11 = new Game
            {
                HostTeam = CountryType.Austria.ToString(),
                GuestTeam = CountryType.Hungary.ToString(),
                Date = new DateTime(2016, 6, 14, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game11);

            var game12 = new Game
            {
                HostTeam = CountryType.Portugal.ToString(),
                GuestTeam = CountryType.Iceland.ToString(),
                Date = new DateTime(2016, 6, 14, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game12);

            var game13 = new Game
            {
                HostTeam = CountryType.Romania.ToString(),
                GuestTeam = CountryType.Switzerland.ToString(),
                Date = new DateTime(2016, 6, 15, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game13);

            var game14 = new Game
            {
                HostTeam = CountryType.France.ToString(),
                GuestTeam = CountryType.Albania.ToString(),
                Date = new DateTime(2016, 6, 15, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game14);

            var game15 = new Game
            {
                HostTeam = CountryType.Russia.ToString(),
                GuestTeam = CountryType.Slovakia.ToString(),
                Date = new DateTime(2016, 6, 15, 16, 00, 00)
            };
            context.Games.AddOrUpdate(game15);

            var game16 = new Game
            {
                HostTeam = CountryType.England.ToString(),
                GuestTeam = CountryType.Wales.ToString(),
                Date = new DateTime(2016, 6, 16, 16, 00, 00)
            };
            context.Games.AddOrUpdate(game16);

            var game17 = new Game
            {
                HostTeam = CountryType.Ukraine.ToString(),
                GuestTeam = CountryType.Northern_Ireland.ToString(),
                Date = new DateTime(2016, 6, 16, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game17);

            var game18 = new Game
            {
                HostTeam = CountryType.Germany.ToString(),
                GuestTeam = CountryType.Poland.ToString(),
                Date = new DateTime(2016, 6, 16, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game18);

            var game19 = new Game
            {
                HostTeam = CountryType.Czech_Republic.ToString(),
                GuestTeam = CountryType.Croatia.ToString(),
                Date = new DateTime(2016, 6, 17, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game19);

            var game20 = new Game
            {
                HostTeam = CountryType.Spain.ToString(),
                GuestTeam = CountryType.Turkey.ToString(),
                Date = new DateTime(2016, 6, 17, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game20);

            var game21 = new Game
            {
                HostTeam = CountryType.Italy.ToString(),
                GuestTeam = CountryType.Sweden.ToString(),
                Date = new DateTime(2016, 6, 17, 16, 00, 00)
            };
            context.Games.AddOrUpdate(game21);

            var game22 = new Game
            {
                HostTeam = CountryType.Belgium.ToString(),
                GuestTeam = CountryType.Republic_of_Ireland.ToString(),
                Date = new DateTime(2016, 6, 18, 16, 00, 00)
            };
            context.Games.AddOrUpdate(game22);

            var game23 = new Game
            {
                HostTeam = CountryType.Iceland.ToString(),
                GuestTeam = CountryType.Hungary.ToString(),
                Date = new DateTime(2016, 6, 18, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game23);

            var game24 = new Game
            {
                HostTeam = CountryType.Portugal.ToString(),
                GuestTeam = CountryType.Austria.ToString(),
                Date = new DateTime(2016, 6, 18, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game24);

            var game25 = new Game
            {
                HostTeam = CountryType.Romania.ToString(),
                GuestTeam = CountryType.Albania.ToString(),
                Date = new DateTime(2016, 6, 19, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game25);

            var game26 = new Game
            {
                HostTeam = CountryType.Switzerland.ToString(),
                GuestTeam = CountryType.France.ToString(),
                Date = new DateTime(2016, 6, 19, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game26);

            var game27 = new Game
            {
                HostTeam = CountryType.Russia.ToString(),
                GuestTeam = CountryType.Wales.ToString(),
                Date = new DateTime(2016, 6, 20, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game27);

            var game28 = new Game
            {
                HostTeam = CountryType.Slovakia.ToString(),
                GuestTeam = CountryType.England.ToString(),
                Date = new DateTime(2016, 6, 20, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game28);

            var game29 = new Game
            {
                HostTeam = CountryType.Ukraine.ToString(),
                GuestTeam = CountryType.Poland.ToString(),
                Date = new DateTime(2016, 6, 21, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game29);

            var game30 = new Game
            {
                HostTeam = CountryType.Northern_Ireland.ToString(),
                GuestTeam = CountryType.Germany.ToString(),
                Date = new DateTime(2016, 6, 21, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game30);

            var game31 = new Game
            {
                HostTeam = CountryType.Czech_Republic.ToString(),
                GuestTeam = CountryType.Turkey.ToString(),
                Date = new DateTime(2016, 6, 21, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game31);

            var game32 = new Game
            {
                HostTeam = CountryType.Croatia.ToString(),
                GuestTeam = CountryType.Spain.ToString(),
                Date = new DateTime(2016, 6, 21, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game32);

            var game33 = new Game
            {
                HostTeam = CountryType.Italy.ToString(),
                GuestTeam = CountryType.Republic_of_Ireland.ToString(),
                Date = new DateTime(2016, 6, 22, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game33);

            var game34 = new Game
            {
                HostTeam = CountryType.Sweden.ToString(),
                GuestTeam = CountryType.Belgium.ToString(),
                Date = new DateTime(2016, 6, 22, 22, 00, 00)
            };
            context.Games.AddOrUpdate(game34);

            var game35 = new Game
            {
                HostTeam = CountryType.Iceland.ToString(),
                GuestTeam = CountryType.Austria.ToString(),
                Date = new DateTime(2016, 6, 22, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game35);

            var game36 = new Game
            {
                HostTeam = CountryType.Hungary.ToString(),
                GuestTeam = CountryType.Portugal.ToString(),
                Date = new DateTime(2016, 6, 22, 19, 00, 00)
            };
            context.Games.AddOrUpdate(game36);
            context.SaveChanges();
        }

        private void SeedUsers(LesGamblers.Data.LesGamblersDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<Gambler>(new UserStore<Gambler>(context));

            userManager.PasswordValidator = new MinimumLengthValidator(5);

            context.Configuration.LazyLoadingEnabled = true;

            var gambler1 = new Gambler
            {
                FirstName = "Martin",
                LastName = "Videv",
                UserName = "MartinVidev@gamb.bg"
            };
            if (userManager.FindByName("MartinVidev@gamb.bg") == null)
            {
                var result = userManager.Create(gambler1, "MartinVidev123");
            }

            var gambler2 = new Gambler
            {
                FirstName = "Nikolay",
                LastName = "Lyubenov",
                UserName = "NikolayLyubenov@gamb.bg"
            };
            if (userManager.FindByName("NikolayLyubenov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler2, gambler2.FirstName + gambler2.LastName + "123");
            }
            
            var gambler5 = new Gambler
            {
                FirstName = "Filip",
                LastName = "Djalov",
                UserName = "FilipDjalov@gamb.bg"
            };
            if (userManager.FindByName("FilipDjalov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler5, gambler5.FirstName + gambler5.LastName + "123");
            }

            var gambler6 = new Gambler
            {
                FirstName = "Hristian",
                LastName = "Haralampiev",
                UserName = "HristianHaralampiev@gamb.bg"
            };
            if (userManager.FindByName("HristianHaralampiev@gamb.bg") == null)
            {
                var result = userManager.Create(gambler6, gambler6.FirstName + gambler6.LastName + "123");
            }

            var gambler7 = new Gambler
            {
                FirstName = "Velislav",
                LastName = "Petrov",
                UserName = "VelislavPetrov@gamb.bg"
            };
            if (userManager.FindByName("VelislavPetrov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler7, gambler7.FirstName + gambler7.LastName + "123");
            }

            var gambler8 = new Gambler
            {
                FirstName = "Yordan",
                LastName = "Peev",
                UserName = "YordanPeev@gamb.bg"
            };
            if (userManager.FindByName("YordanPeev@gamb.bg") == null)
            {
                var result = userManager.Create(gambler8, gambler8.FirstName + gambler8.LastName + "123");
            }

            var gambler9 = new Gambler
            {
                FirstName = "Kaloyan",
                LastName = "Kirilov",
                UserName = "KaloyanKirilov@gamb.bg"
            };
            if (userManager.FindByName("KaloyanKirilov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler9, gambler9.FirstName + gambler9.LastName + "123");
            }

            var gambler10 = new Gambler
            {
                FirstName = "Yulian",
                LastName = "Velichkov",
                UserName = "YulianVelichkov@gamb.bg"
            };
            if (userManager.FindByName("YulianVelichkov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler10, gambler10.FirstName + gambler10.LastName + "123");
            }

            var gambler11 = new Gambler
            {
                FirstName = "Veselin",
                LastName = "Doichev",
                UserName = "VeselinDoichev@gamb.bg"
            };
            if (userManager.FindByName("VeselinDoichev@gamb.bg") == null)
            {
                var result = userManager.Create(gambler11, gambler11.FirstName + gambler11.LastName + "123");
            }

            var gambler12 = new Gambler
            {
                FirstName = "Martin",
                LastName = "Vasev",
                UserName = "MartinVasev@gamb.bg"
            };
            if (userManager.FindByName("MartinVasev@gamb.bg") == null)
            {
                var result = userManager.Create(gambler12, gambler12.FirstName + gambler12.LastName + "123");
            }

            var gambler13 = new Gambler
            {
                FirstName = "Boyan",
                LastName = "Kuzov",
                UserName = "BoyanKuzov@gamb.bg"
            };
            if (userManager.FindByName("BoyanKuzov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler13, gambler13.FirstName + gambler13.LastName + "123");
            }

            var gambler14 = new Gambler
            {
                FirstName = "Viktor",
                LastName = "Kalaikov",
                UserName = "ViktorKalaikov@gamb.bg"
            };
            if (userManager.FindByName("ViktorKalaikov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler14, gambler14.FirstName + gambler14.LastName + "123");
            }

            var gambler15 = new Gambler
            {
                FirstName = "Rumen",
                LastName = "Ivanov",
                UserName = "RumenIvanov@gamb.bg"
            };
            if (userManager.FindByName("RumenIvanov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler15, gambler15.FirstName + gambler15.LastName + "123");
            }

            var gambler16 = new Gambler
            {
                FirstName = "Lyubomir",
                LastName = "Dyankov",
                UserName = "LyubomirDyankov@gamb.bg"
            };
            if (userManager.FindByName("LyubomirDyankov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler16, gambler16.FirstName + gambler16.LastName + "123");
            }

            var gambler17 = new Gambler
            {
                FirstName = "Denis",
                LastName = "Chavdarov",
                UserName = "DenisChavdarov@gamb.bg"
            };
            if (userManager.FindByName("DenisChavdarov@gamb.bg") == null)
            {
                var result = userManager.Create(gambler17, gambler17.FirstName + gambler17.LastName + "123");
            }
            context.SaveChanges();
        }
    }
}
