namespace LesGamblers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Games;

    public class HomeController : Controller
    {
        private IGamesService games;

        public HomeController(IGamesService games)
        {
            this.games = games;
        }

        public ActionResult Index()
        {
            var allDataGames = this.games.GetAll();
            var allGames = new List<ListAllGamesPredictions>();
            foreach (var game in allDataGames)
            {
                var newGame = this.games.GetById(game.Id).ProjectTo<ListAllGamesPredictions>().FirstOrDefault();
                allGames.Add(newGame);
            }

            return View(allGames);
        }
    }
}