namespace LesGamblers.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<LesGamblers.Data.LesGamblersDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LesGamblers.Data.LesGamblersDbContext context)
        {
            if (context.Gamblers.Count() == 0)
            {
                SeedGamblers(context);
            }

            if (context.Games.Count() == 0)
            {
                SeedGames(context);
            }
        }

        private void SeedGames(LesGamblersDbContext context)
        {
            context.Configuration.LazyLoadingEnabled = true;

            var game1 = new Game
            {
                HostTeam = CountryType.France.ToString(),
                GuestTeam = CountryType.Romania.ToString()
            };
            context.Games.Add(game1);

            var game2 = new Game
            {
                HostTeam = CountryType.Albania.ToString(),
                GuestTeam = CountryType.Switzerland.ToString()
            };
            context.Games.Add(game2);

            var game3 = new Game
            {
                HostTeam = CountryType.Wales.ToString(),
                GuestTeam = CountryType.Slovakia.ToString()
            };
            context.Games.Add(game3);

            var game4 = new Game
            {
                HostTeam = CountryType.England.ToString(),
                GuestTeam = CountryType.Russia.ToString()
            };
            context.Games.Add(game4);

            var game5 = new Game
            {
                HostTeam = CountryType.Poland.ToString(),
                GuestTeam = CountryType.Northern_Ireland.ToString()
            };
            context.Games.Add(game5);

            var game6 = new Game
            {
                HostTeam = CountryType.Germany.ToString(),
                GuestTeam = CountryType.Ukraine.ToString()
            };
            context.Games.Add(game6);

            var game7 = new Game
            {
                HostTeam = CountryType.Turkey.ToString(),
                GuestTeam = CountryType.Croatia.ToString()
            };
            context.Games.Add(game7);

            var game8 = new Game
            {
                HostTeam = CountryType.Spain.ToString(),
                GuestTeam = CountryType.Czech_Republic.ToString()
            };
            context.Games.Add(game8);

            var game9 = new Game
            {
                HostTeam = CountryType.Republic_of_Ireland.ToString(),
                GuestTeam = CountryType.Sweden.ToString()
            };
            context.Games.Add(game9);

            var game10 = new Game
            {
                HostTeam = CountryType.Belgium.ToString(),
                GuestTeam = CountryType.Italy.ToString()
            };
            context.Games.Add(game10);

            var game11 = new Game
            {
                HostTeam = CountryType.Austria.ToString(),
                GuestTeam = CountryType.Hungary.ToString()
            };
            context.Games.Add(game11);

            var game12 = new Game
            {
                HostTeam = CountryType.Portugal.ToString(),
                GuestTeam = CountryType.Iceland.ToString()
            };
            context.Games.Add(game12);

            var game13 = new Game
            {
                HostTeam = CountryType.Romania.ToString(),
                GuestTeam = CountryType.Switzerland.ToString()
            };
            context.Games.Add(game13);

            var game14 = new Game
            {
                HostTeam = CountryType.France.ToString(),
                GuestTeam = CountryType.Albania.ToString()
            };
            context.Games.Add(game14);

            var game15 = new Game
            {
                HostTeam = CountryType.Russia.ToString(),
                GuestTeam = CountryType.Slovakia.ToString()
            };
            context.Games.Add(game15);

            var game16 = new Game
            {
                HostTeam = CountryType.England.ToString(),
                GuestTeam = CountryType.Wales.ToString()
            };
            context.Games.Add(game16);

            var game17 = new Game
            {
                HostTeam = CountryType.Ukraine.ToString(),
                GuestTeam = CountryType.Northern_Ireland.ToString()
            };
            context.Games.Add(game17);

            var game18 = new Game
            {
                HostTeam = CountryType.Germany.ToString(),
                GuestTeam = CountryType.Poland.ToString()
            };
            context.Games.Add(game18);

            var game19 = new Game
            {
                HostTeam = CountryType.Czech_Republic.ToString(),
                GuestTeam = CountryType.Croatia.ToString()
            };
            context.Games.Add(game19);

            var game20 = new Game
            {
                HostTeam = CountryType.Spain.ToString(),
                GuestTeam = CountryType.Turkey.ToString()
            };
            context.Games.Add(game20);

            var game21 = new Game
            {
                HostTeam = CountryType.Italy.ToString(),
                GuestTeam = CountryType.Sweden.ToString()
            };
            context.Games.Add(game21);

            var game22 = new Game
            {
                HostTeam = CountryType.Belgium.ToString(),
                GuestTeam = CountryType.Republic_of_Ireland.ToString()
            };
            context.Games.Add(game22);

            var game23 = new Game
            {
                HostTeam = CountryType.Iceland.ToString(),
                GuestTeam = CountryType.Hungary.ToString()
            };
            context.Games.Add(game23);

            var game24 = new Game
            {
                HostTeam = CountryType.Portugal.ToString(),
                GuestTeam = CountryType.Austria.ToString()
            };
            context.Games.Add(game24);

            var game25 = new Game
            {
                HostTeam = CountryType.Romania.ToString(),
                GuestTeam = CountryType.Albania.ToString()
            };
            context.Games.Add(game25);

            var game26 = new Game
            {
                HostTeam = CountryType.Switzerland.ToString(),
                GuestTeam = CountryType.France.ToString()
            };
            context.Games.Add(game26);

            var game27 = new Game
            {
                HostTeam = CountryType.Russia.ToString(),
                GuestTeam = CountryType.Wales.ToString()
            };
            context.Games.Add(game27);

            var game28 = new Game
            {
                HostTeam = CountryType.Slovakia.ToString(),
                GuestTeam = CountryType.England.ToString()
            };
            context.Games.Add(game28);

            var game29 = new Game
            {
                HostTeam = CountryType.Ukraine.ToString(),
                GuestTeam = CountryType.Poland.ToString()
            };
            context.Games.Add(game29);

            var game30 = new Game
            {
                HostTeam = CountryType.Northern_Ireland.ToString(),
                GuestTeam = CountryType.Germany.ToString()
            };
            context.Games.Add(game30);

            var game31 = new Game
            {
                HostTeam = CountryType.Czech_Republic.ToString(),
                GuestTeam = CountryType.Turkey.ToString()
            };
            context.Games.Add(game31);

            var game32 = new Game
            {
                HostTeam = CountryType.Croatia.ToString(),
                GuestTeam = CountryType.Spain.ToString()
            };
            context.Games.Add(game32);

            var game33 = new Game
            {
                HostTeam = CountryType.Italy.ToString(),
                GuestTeam = CountryType.Republic_of_Ireland.ToString()
            };
            context.Games.Add(game33);

            var game34 = new Game
            {
                HostTeam = CountryType.Sweden.ToString(),
                GuestTeam = CountryType.Belgium.ToString()
            };
            context.Games.Add(game34);

            var game35 = new Game
            {
                HostTeam = CountryType.Iceland.ToString(),
                GuestTeam = CountryType.Austria.ToString()
            };
            context.Games.Add(game35);

            var game36 = new Game
            {
                HostTeam = CountryType.Hungary.ToString(),
                GuestTeam = CountryType.Portugal.ToString()
            };
            context.Games.Add(game36);
            context.SaveChanges();
        }

        private void SeedGamblers(LesGamblers.Data.LesGamblersDbContext context)
        {
            context.Configuration.LazyLoadingEnabled = true;

            var gambler1 = new Gambler
            {
                FirstName = "Martin",
                LastName = "Videv"
            };
            context.Gamblers.Add(gambler1);

            var gambler2 = new Gambler
            {
                FirstName = "Nikolay",
                LastName = "Lyubenov"
            };
            context.Gamblers.Add(gambler2);

            var gambler3 = new Gambler
            {
                FirstName = "Teodor",
                LastName = "Todorov"
            };
            context.Gamblers.Add(gambler3);

            var gambler4 = new Gambler
            {
                FirstName = "Martin",
                LastName = "Atanasov"
            };
            context.Gamblers.Add(gambler4);

            var gambler5 = new Gambler
            {
                FirstName = "Filip",
                LastName = "Djalov"
            };
            context.Gamblers.Add(gambler5);

            var gambler6 = new Gambler
            {
                FirstName = "Hristian",
                LastName = "Haralampiev"
            };
            context.Gamblers.Add(gambler6);

            var gambler7 = new Gambler
            {
                FirstName = "Velislav",
                LastName = "Petrov"
            };
            context.Gamblers.Add(gambler7);

            var gambler8 = new Gambler
            {
                FirstName = "Yordan",
                LastName = "Peev"
            };
            context.Gamblers.Add(gambler8);

            var gambler9 = new Gambler
            {
                FirstName = "Kaloyan",
                LastName = "Kirilov"
            };
            context.Gamblers.Add(gambler9);

            var gambler10 = new Gambler
            {
                FirstName = "Yulian",
                LastName = "Velichkov"
            };
            context.Gamblers.Add(gambler10);

            var gambler11 = new Gambler
            {
                FirstName = "Veselin",
                LastName = "Doichev"
            };
            context.Gamblers.Add(gambler11);

            var gambler12 = new Gambler
            {
                FirstName = "Martin",
                LastName = "Vasev"
            };
            context.Gamblers.Add(gambler12);

            var gambler13 = new Gambler
            {
                FirstName = "Boyan",
                LastName = "Kuzov"
            };
            context.Gamblers.Add(gambler13);

            var gambler14 = new Gambler
            {
                FirstName = "Viktor",
                LastName = "Kalaikov"
            };
            context.Gamblers.Add(gambler14);

            var gambler15 = new Gambler
            {
                FirstName = "Rumen",
                LastName = "Ivanov"
            };
            context.Gamblers.Add(gambler15);

            var gambler16 = new Gambler
            {
                FirstName = "Lyubomir",
                LastName = "Dyankov"
            };
            context.Gamblers.Add(gambler16);

            var gambler17 = new Gambler
            {
                FirstName = "Denis",
                LastName = "Chavdarov"
            };
            context.Gamblers.Add(gambler17);
            context.SaveChanges();
        }
    }
}
