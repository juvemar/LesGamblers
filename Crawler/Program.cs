namespace Crawler
{
    using System.Linq;
    using System.Text;

    using AngleSharp;

    using LesGamblers.Data;
    using LesGamblers.Data.Repositories;
    using LesGamblers.Models;
    using LesGamblers.Services;

    public static class Program
    {
        private static ILesGamblersDbContext db = new LesGamblersDbContext();

        public static void Main() 
        {
            IRepository<Team> repo = new Repository<Team>(db);
            var teamsServices = new TeamsService(repo);
  
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
                //teamsServices.Add(newTeam);
                var currentTeam = documentCurrentTeam.QuerySelectorAll(".squad--stats a").Select(x => x.TextContent).ToList();
                foreach (var player in currentTeam)
                {
                    SavePlayer(player, newTeam);
                }
            }
        }

        private static void SavePlayer(string playerData, Team team)
        {
            IRepository<Player> repo = new Repository<Player>(db);
            var playersServices = new PlayersService(repo);
  
            var formatted = playerData.Replace(" ", string.Empty);
            var bracketIndex = formatted.IndexOf('(');
            var playerName = formatted.Substring(0, bracketIndex);
            var bracketEndIndex = formatted.IndexOf(')');
            var playerNumber = 0;
            var strNumber = formatted.Substring(bracketIndex + 1, bracketEndIndex - bracketIndex - 1);
            var parseNumber = int.TryParse(strNumber, out playerNumber);

            //playersServices.Add(new Player
            //    {
            //        Number = playerNumber,
            //        Name = playerName
            //    });
        }
    }
}
