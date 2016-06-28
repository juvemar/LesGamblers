namespace LesGamblers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Games;
    using LesGamblers.Web.Models;
    using LesGamblers.Web.Models.Gamblers;

    public class HomeController : Controller
    {
        private IGamesService games;
        private IGamblersService gamblers;

        public HomeController(IGamesService games, IGamblersService gamblers)
        {
            this.games = games;
            this.gamblers = gamblers;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            var allDataGames = this.games.GetAll()
                .OrderBy(g => g.Date)
                .ToList();
            model.AllGames = new List<SelectListItem>();
            foreach (var game in allDataGames)
            {
                var newGame = new SelectListItem
                {
                    Text = game.Date.ToString("dd.MM.yy HH:mm") + "  |  " + game.HostTeam.Replace('_', ' ') + " - " + game.GuestTeam.Replace('_', ' '),
                    Value = game.Id.ToString()
                };
                model.AllGames.Add(newGame);
            }

            var allDataGamblers = this.gamblers.GetAll()
                .OrderBy(g => g.FirstName)
                .ThenBy(g => g.LastName)
                .ToList();
            model.AllGamblers = new List<SelectListItem>();
            foreach (var gambler in allDataGamblers)
            {
                var newGambler = new SelectListItem
                {
                    Text = gambler.FirstName + " " + gambler.LastName,
                    Value = gambler.UserName
                };
                model.AllGamblers.Add(newGambler);
            }

            return View(model);
        }
    }
}