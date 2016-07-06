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

            var allGamblers = this.gamblers
                            .GetAll()
                            .OrderByDescending(g => g.TotalPoints)
                            .ThenByDescending(g => g.FinalResultsPredicted)
                            .ToList();

            var gamblersModel = new List<CheckGamblersPredictionsViewModel>();

            foreach (var gambler in allGamblers)
            {
                var newGambler = new CheckGamblersPredictionsViewModel()
                {
                    UserName = gambler.UserName,
                    FirstName = gambler.FirstName,
                    LastName = gambler.LastName,
                    TotalPoints = gambler.TotalPoints,
                    FinalResultsPredicted = gambler.FinalResultsPredicted,
                    GoalscorersPredicted = gambler.GoalscorersPredicted,
                    SignsPredicted = gambler.SignsPredicted
                };

                gamblersModel.Add(newGambler);
            }

            return this.View(gamblersModel);
        }
    }
}