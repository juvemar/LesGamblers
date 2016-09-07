namespace Crawler
{
    using System.Collections.Generic;
    using System.Linq;

    using AngleSharp;

    using LesGamblers.Data;
    using LesGamblers.Data.Repositories;
    using LesGamblers.Models;
    using LesGamblers.Services;
    using LesGamblers.Services.Contracts;

    public class DataSeederEuroFinals2016 : TeamsDataSeeder, ICrawler
    {
        public void CrawlTeamsData()
        {
            if (this.TeamsService.GetAll().Count() > 0)
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
                if (this.TeamsService.GetAll().FirstOrDefault(x => x.Name == countryName) != null)
                {
                    continue;
                }

                var currentTeamPlayers = documentCurrentTeam
                    .QuerySelectorAll(".squad--stats a")
                    .Select(x => x.TextContent)
                    .ToList();
                var currentTeamPlayersClubs = documentCurrentTeam
                    .QuerySelectorAll(".squad--stats a")
                    .Select(x => x.ParentElement
                                .NextElementSibling
                                .NextElementSibling
                                .NextElementSibling
                                .TextContent)
                    .ToList();

                this.SeedTeam(countryName, currentTeamPlayers, currentTeamPlayersClubs, true);
            }
        }
    }
}
