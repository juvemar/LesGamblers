namespace LesGamblers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Gamblers;

    public class GamblersController : Controller
    {
        private const int NumberPredictionsToShow = 16;
        private IGamblersService gamblers;
        private IGamesService games;

        public GamblersController(IGamblersService gamblers, IGamesService games)
        {
            this.gamblers = gamblers;
            this.games = games;
        }

        [HttpGet]
        public ActionResult CheckGamblersPredictions()
        {
            var allDataGames = this.games.GetAll()
                .OrderByDescending(g => g.Date)
                .Take(NumberPredictionsToShow)
                .OrderBy(g => g.Date)
                .ToList();

            var model = new ListGamblersViewModel();
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

        [HttpGet]
        public ActionResult UpdateGambler()
        {
            var model = new UpdateGamblerViewModel();
            model.AllGamblers = new List<SelectListItem>();

            var allGamblers = this.gamblers.GetAll().ToList();
            foreach (var gambler in allGamblers)
            {
                model.AllGamblers.Add(new SelectListItem
                    {
                        Text = gambler.FirstName + " " + gambler.LastName + " | " + gambler.Email,
                        Value = gambler.UserName
                    });
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
        public ActionResult UpdateGambler(UpdateGamblerViewModel model)
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
                //this.gamblers.ChangeGamblerPoints(updatedGambler, gambler.Id); // Change exact gambler points manually
                if (model.MakeAdmin)
                {
                    if (updatedGambler.Roles.Count < 1)
                    {
                        this.gamblers.ChangeUserRole(gambler.Id, "admin");
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}