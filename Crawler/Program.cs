﻿namespace Crawler
{
    using System.Linq;

    using AngleSharp;

    using LesGamblers.Data;
    using LesGamblers.Data.Repositories;
    using LesGamblers.Models;
    using LesGamblers.Services;
    using LesGamblers.Services.Contracts;

    public static class Program
    {
        private static ILesGamblersDbContext Db = new LesGamblersDbContext();
        private static IRepository<Team> Repo = new Repository<Team>(Db);
        private static ITeamsService TeamsService = new TeamsService(Repo);

        public static void Main() 
        {
            SeedEuroFinals2016();
        }

        private static void SeedEuroFinals2016()
        {
            if (TeamsService.GetAll().Count() > 0)
            {
                return;
            }

            var configuration = Configuration.Default.WithDefaultLoader();
            var browsingContext = BrowsingContext.New(configuration);

            var url = "http://www.uefa.com/uefaeuro/season=2016/teams/index.html";
            var documentAllTeams = browsingContext.OpenAsync(url).Result;
            var allTeamsUrls = documentAllTeams.QuerySelectorAll(".teams--qualified a")
                .Select(x => x.Attributes)
                .ToList()
                .SelectMany(x => x.Where(y => y.Name == "href"))
                .Select(x => x.Value)
                .ToList();

            foreach (var currentUrl in allTeamsUrls)
            {
                var indexOfSquad = currentUrl.IndexOf("/index.html");
                var documentCurrentTeam = browsingContext.OpenAsync("http://www.uefa.com" + currentUrl.Insert(indexOfSquad, "/squad")).Result;
                var countryName = documentCurrentTeam.QuerySelector(".team-name").TextContent;
                var newTeam = new Team
                {
                    Name = countryName
                };
                TeamsService.Add(newTeam);
                var currentTeamPlayers = documentCurrentTeam.QuerySelectorAll(".squad--stats a").Select(x => x.TextContent).ToList();
                var currentTeamPlayersClubs = documentCurrentTeam.QuerySelectorAll(".squad--stats a").Select(x => x.ParentElement.NextElementSibling.NextElementSibling.NextElementSibling.TextContent).ToList();
                var playersCount = 0;
                foreach (var player in currentTeamPlayers)
                {
                    var playersClub = currentTeamPlayersClubs[playersCount];
                    SavePlayer(player, newTeam, playersClub);
                    playersCount++;
                }
            }
        }

        private static void SavePlayer(string playerData, Team team, string playersClub)
        {
            IRepository<Player> repo = new Repository<Player>(Db);
            var playersServices = new PlayersService(repo);
  
            var formatted = playerData.Replace(" ", string.Empty);
            var bracketIndex = formatted.IndexOf('(');
            var playerName = formatted.Substring(0, bracketIndex);
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
            //TeamsService.AddPlayer(team, addPlayer);
        }
    }
}