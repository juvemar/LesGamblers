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
        private IPredictionsService predictions;

        public HomeController(IGamesService games, IGamblersService gamblers, IPredictionsService predictions)
        {
            this.games = games;
            this.gamblers = gamblers;
            this.predictions = predictions;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var allGamblers = this.gamblers.GetAll().ToList();
            var gamblersModel = new List<CheckGamblersPredictionsViewModel>();

            foreach (var gambler in allGamblers)
            {
                var gamblerPredictions = this.predictions.GetAll().Where(p => p.GamblerId == gambler.Id).ToList();
                var totalPoints = gamblerPredictions.Sum(p => p.TotalPoints);
                var exactResults = gamblerPredictions.Where(p => p.FinalResultPredicted == true).Count();
                var goalscorers = gamblerPredictions.Where(p => p.GoalscorerPredicted == true).Count();
                var signs = gamblerPredictions.Where(p => p.SignPredicted == true).Count();

                var newGambler = new CheckGamblersPredictionsViewModel()
                {
                    UserName = gambler.UserName,
                    FirstName = gambler.FirstName,
                    LastName = gambler.LastName,
                    TotalPoints = totalPoints,
                    FinalResultsPredicted = exactResults,
                    GoalscorersPredicted = goalscorers,
                    SignsPredicted = signs
                };

                gamblersModel.Add(newGambler);
            }

            return this.View(gamblersModel
                            .OrderByDescending(g => g.TotalPoints)
                            .ThenByDescending(g => g.FinalResultsPredicted)
                            .ToList());
        }

        [HttpGet]
        public ActionResult DeleteEntities()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult DeleteEntities(DeleteEntitiesViewModel model)
        {
            if (model.DeleteGames)
            {
                this.games.DeleteAll();
            }

            if (model.DeletePredictions)
            {
                this.predictions.DeleteAll();   
            }

            return RedirectToAction("Index", "Home");
        }
    }
}