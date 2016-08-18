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
    }
}
