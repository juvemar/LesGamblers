namespace Crawler
{
    using System.Collections.Generic;

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

        protected void SeedTeam(string teamName, List<string> playersNames, List<string> playersClubs)
        {
            var newTeam = new Team
            {
                Name = teamName
            };
            this.TeamsService.Add(newTeam);

            var playersCount = 0;
            foreach (var player in playersNames)
            {
                var playersClub = playersClubs[playersCount];
                this.SeedPlayer(player, newTeam, playersClub);
                playersCount++;
            }
        }

        protected virtual void SeedPlayer(string playerData, Team team, string playersClub)
        {
            IRepository<Player> repo = new Repository<Player>(Db);
            var playersServices = new PlayersService(repo);

            var formatted = playerData.Trim();
            var bracketIndex = formatted.IndexOf('(');
            var playerName = formatted.Substring(0, bracketIndex).Trim();
            var bracketEndIndex = formatted.IndexOf(')');
            var playerNumber = 0;
            var strNumber = formatted.Substring(bracketIndex + 1, bracketEndIndex - bracketIndex - 1);
            var parseNumber = int.TryParse(strNumber, out playerNumber);

            var addPlayer = new Player
            {
                Number = playerNumber,
                Name = playerName,
                ClubTeam = playersClub,
                TeamId = team.Id,
                Country = team.Name
            };
            playersServices.Add(addPlayer);
        }
    }
}
