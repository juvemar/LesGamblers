namespace Crawler
{
    using System.Collections.Generic;
    using System.Linq;

    using LesGamblers.Data;
    using LesGamblers.Data.Repositories;
    using LesGamblers.Models;
    using LesGamblers.Services;
    using LesGamblers.Services.Contracts;

    public abstract class TeamsDataSeeder
    {
        protected ITeamsService TeamsService;
        private ILesGamblersDbContext Db;
        private IRepository<Team> Repo;

        public TeamsDataSeeder()
        {
            this.Db = new LesGamblersDbContext();
            this.Repo = new Repository<Team>(this.Db);
            this.TeamsService = new TeamsService(this.Repo);
        }

        protected void SeedTeam(string teamName, List<string> playersNames, List<string> playersSecondClubs, bool playerNameWithBrackets)
        {
            var newTeam = new Team
            {
                Name = teamName
            };
            this.TeamsService.Add(newTeam);

            var playersCount = 0;
            foreach (var player in playersNames)
            {
                var playersClub = playersSecondClubs[playersCount];
                this.SeedPlayer(player, newTeam, playersClub);
                playersCount++;
            }
        }

        private void SeedPlayer(string playerData, Team team, string playersClub)
        {
            IRepository<Player> repo = new Repository<Player>(Db);
            var playersServices = new PlayersService(repo);

            var addPlayer = new Player
            {
                Name = this.GetPlayerName(playerData),
                SecondTeam = playersClub,
                TeamId = team.Id,
                CurrentTeam = team.Name
            };
            playersServices.Add(addPlayer);
        }

        private string GetPlayerName(string playerData)
        {
            if (playerData.Contains("("))
            {
                var formatted = playerData.Trim();
                var bracketIndex = formatted.IndexOf('(');
                var playerName = formatted.Substring(0, bracketIndex).Trim();
                return playerName;
            }

            return playerData;
        }
    }
}
