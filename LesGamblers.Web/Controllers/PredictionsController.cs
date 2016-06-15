﻿namespace LesGamblers.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using LesGamblers.Web.Models.Predictions;
    using LesGamblers.Services.Contracts;

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

        public ActionResult AddGambler(AddPredictionViewModel model)
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