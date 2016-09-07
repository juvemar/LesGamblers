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

    public class DataSeederCL20162017 : TeamsDataSeeder, ICrawler
    {
        public void CrawlTeamsData()
        {
            if (this.TeamsService.GetAll().Count() > 0)
            {
                return;
            }

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

                var teamHref = "http://www.uefa.com" + team
                    .Attributes
                    .Where(x => x.Name == "href")
                    .Select(x => x.Value)
                    .FirstOrDefault()
                    .ToString();

                var squadIndex = teamHref.IndexOf("index.html");
                var playersHref = teamHref.Insert(squadIndex, "squad/");

                var documentCurrentTeamPlayers = browsingContext.OpenAsync(playersHref).Result;
                var currentTeamPlayers = documentCurrentTeamPlayers
                                .QuerySelectorAll(".medTitle a")
                                .Select(x => x.TextContent)
                                .ToList();

                var currentTeamCountries = documentCurrentTeamPlayers
                                .QuerySelectorAll("tr td.l")
                                .Select(x => x.TextContent)
                                .Where(x => x.Length == 3)
                                .ToList();

                var teamName = team
                    .Children
                    .FirstOrDefault()
                    .Attributes
                    .Where(x => x.Name == "title")
                    .Select(x => x.Value)
                    .FirstOrDefault();

                this.SeedTeam(teamName, currentTeamPlayers, currentTeamCountries, false);
            }
        }
    }
}
