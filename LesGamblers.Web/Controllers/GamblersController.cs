namespace LesGamblers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Gamblers;

    public class GamblersController : Controller
    {
        private IGamblersService gamblers;

        public GamblersController(IGamblersService gamblers)
        {
            this.gamblers = gamblers;
        }

        [HttpGet]
        public ActionResult ListGamblersResults()
        {
            var allGamblers = this.gamblers
                            .GetAll()
                            .OrderByDescending(g => g.TotalPoints)
                            .ThenByDescending(g => g.FinalResultsPredicted)
                            .ToList();

            var gamblersModel = new List<ListGamblersViewModel>();

            foreach (var gambler in allGamblers)
            {
                var newGambler = new ListGamblersViewModel()
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

        [HttpGet]
        public ActionResult UpdateGamblerPoints()
        {
            var model = new UpdateGamblerViewModel();
            model.AllGamblers = new List<SelectListItem>();

            var allGamblers = this.gamblers.GetAll().ToList();
            foreach (var gambler in allGamblers)
            {
                model.AllGamblers.Add(new SelectListItem
                    {
                        Text = gambler.FirstName + " " + gambler.LastName,
                        Value = gambler.UserName
                    });
            }

            return this.View(model);
        }

        [HttpPost]
        public ActionResult UpdateGamblerPoints(UpdateGamblerViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.AllGamblers = new List<SelectListItem>();

                var allGamblers = this.gamblers.GetAll().ToList();
                foreach (var current in allGamblers)
                {
                    model.AllGamblers.Add(new SelectListItem
                    {
                        Text = current.FirstName + " " + current.LastName,
                        Value = current.UserName
                    });
                }
                return this.View(model);
            }

            var gambler = this.gamblers.GetByUsername(model.UserName).FirstOrDefault();
            if (gambler != null)
            {
                var updatedGambler = AutoMapper.Mapper.Map<UpdateGamblerViewModel, LesGamblers.Models.Gambler>(model);
                this.gamblers.UpdateGambler(updatedGambler, gambler.Id);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}