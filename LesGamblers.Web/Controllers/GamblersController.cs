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
                            .OrderBy(g => g.TotalPoints)
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
    }
}