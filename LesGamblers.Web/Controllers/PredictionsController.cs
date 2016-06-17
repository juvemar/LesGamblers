namespace LesGamblers.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using LesGamblers.Web.Models.Predictions;
    using LesGamblers.Services.Contracts;
    using LesGamblers.Models;

    public class PredictionsController : Controller
    {
        private IGamblersService gamblers;
        private IPredictionsService predictions;
        private IGamesService games;

        public PredictionsController(IGamblersService gamblers, IPredictionsService predictions, IGamesService games)
        {
            this.gamblers = gamblers;
            this.predictions = predictions;
            this.games = games;
        }

        //[Authorize]
        [HttpGet]
        public ActionResult AddPrediction(AddPredictionViewModel model)
        {
            var availableGames = this.games
                                .GetAll()
                                .Where(g => string.IsNullOrEmpty(g.FinalResult))
                                .OrderBy(g => g.Date)
                                .ToList();

            model.Games = new List<SelectListItem>();
            foreach (var game in availableGames)
            {
                model.Games.Add(new SelectListItem
                {
                    Text = game.Date.ToString("dd.MM.yy HH:mm") + "  |  " + game.HostTeam + " - " + game.GuestTeam,
                    Value = game.HostTeam + " " + game.GuestTeam
                });
            }
            model.Players = new List<SelectListItem>();

            return this.View(model);
        }

        //[Authorize]
        [HttpPost]
        public ActionResult AddPredictionPost(AddPredictionViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var dataModel = AutoMapper.Mapper.Map<AddPredictionViewModel, LesGamblers.Models.Prediction>(model);

            var currentGambler = this.gamblers.GetByUsername(this.User.Identity.Name).FirstOrDefault();
            dataModel.Gambler = currentGambler;

            this.predictions.Add(dataModel);

            this.TempData["Notification"] = "Your prediction was added successfully! Good luck!";
            return RedirectToAction("Index", "Home");
        }
    }
}