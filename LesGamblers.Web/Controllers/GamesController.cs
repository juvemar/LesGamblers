namespace LesGamblers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Games;
    using System;

    public class GamesController : Controller
    {
        private ITeamsService teams;
        private IGamesService games;

        public GamesController(ITeamsService teams, IGamesService games)
        {
            this.teams = teams;
            this.games = games;
        }

        [HttpGet]
        //[Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
        public ActionResult AddGame(AddGameViewModel model)
        {
            var allTeams = this.teams.GetAll().ToList();

            model.Teams = new List<SelectListItem>();
            foreach (var team in allTeams)
            {
                model.Teams.Add(new SelectListItem
                {
                    Text = team.Name,
                    Value = team.Name
                });
            }

            return this.View(model);
        }

        [HttpPost]
        [ActionName("AddGame")]
        //[Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
        public ActionResult AddGamePost(AddGameViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var dataModel = AutoMapper.Mapper.Map<AddGameViewModel, LesGamblers.Models.Game>(model);
            this.games.Add(dataModel);
            this.TempData["Notification"] = "Your game was added successfully! Good luck!";

            return RedirectToAction("Index", "Home");
        }
    }
}