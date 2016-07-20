namespace Crawler
{
    using System.Linq;

    using AngleSharp;

    using LesGamblers.Data;
    using LesGamblers.Data.Repositories;
    using LesGamblers.Models;
    using LesGamblers.Services;
    using LesGamblers.Services.Contracts;
    using System.Collections.Generic;

    public static class StartUp
    {
        private static ILesGamblersDbContext Db = new LesGamblersDbContext();
        private static IRepository<Team> Repo = new Repository<Team>(Db);
        private static ITeamsService TeamsService = new TeamsService(Repo);

        public static void Main() 
        {
            SeedEuroFinals2016();
            //SeedChampionsLeage20162017();
        }

        private static void SeedChampionsLeage20162017()
        {
            var configuration = Configuration.Default.WithDefaultLoader();
            var browsingContext = BrowsingContext.New(configuration);
            var url = "http://www.uefa.com/uefachampionsleague/season=2017/clubs/index.html";
            var documentAllTeams = browsingContext.OpenAsync(url).Result;

            var allTeamsElements = new List<string>();
            var allTeamsDiv = documentAllTeams.QuerySelectorAll(".clubList").FirstOrDefault();
            foreach (var teamListItem in allTeamsDiv.Children)
            {
                var team = teamListItem
                    .Children.FirstOrDefault();

                var teamHref = team.Attributes
                    .Where(x => x.Name == "href")
                    .Select(x => x.Value)
                    .FirstOrDefault();

                var teamName = team
                    .Children.FirstOrDefault()
                    .Attributes.Where(x => x.Name == "title")
                    .Select(x => x.Value)
                    .FirstOrDefault();

                var newTeam = new Team
                {
                    Name = teamName
                };
            }

            if (allTeamsDiv != null)
            {
                var imgs = allTeamsDiv.ChildNodes
               .Where(x => x.NodeName == "img")
               .ToList();
            }
        }

        private static void SeedEuroFinals2016()
        {
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
                if (TeamsService.GetAll().FirstOrDefault(x => x.Name == countryName) != null)
                {
                    continue;
                }
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
            //TeamsService.AddPlayer(team, addPlayer);
        }
    }
}
