namespace LesGamblers.Web.Controllers
{
    using System.Collections.Generic;
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

        //[Authorize]
        [HttpGet]
        public ActionResult AddPet(AddPredictionViewModel model)
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
                    Text = game.HostTeam + ":" + game.GuestTeam + " - " + game.Date.ToString("dd.MM.yyyy hh:mm"),
                    Value = game.Id.ToString()
                });
            }

            return View();
        }

        //[Authorize]
        [HttpPost]
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