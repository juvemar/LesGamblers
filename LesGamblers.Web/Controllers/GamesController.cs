namespace LesGamblers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Games;
    using System;
    using System.Web.Security;

    public class GamesController : Controller
    {
        private ITeamsService teams;
        private IGamesService games;
        private IPlayersService players;

        public GamesController(ITeamsService teams, IGamesService games, IPlayersService players)
        {
            this.teams = teams;
            this.games = games;
            this.players = players;
        }

        [HttpGet]
        [Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
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
        [Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
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

        [HttpGet]
        [Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
        public ActionResult UpdateFinishedGame(UpdateFinishedGameViewModel model)
        {
            var allTeams = this.games.GetAll()
                            .Where(g => g.Date < DateTime.Now)
                            .ToList();

            model.Games = new List<SelectListItem>();
            foreach (var game in allTeams)
            {
                model.Games.Add(new SelectListItem
                {
                    Text = game.Date.ToString("dd.MM.yy HH:mm") + "  |  " + game.HostTeam.Replace('_', ' ') + " - " + game.GuestTeam.Replace('_', ' ') + " " + game.FinalResult,
                    Value = game.Id.ToString()
                });
            }

            return this.View(model);
        }

        [HttpPost]
        [ActionName("UpdateFinishedGame")]
        [Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
        public ActionResult UpdateFinishedGamePost(UpdateFinishedGameViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var isAdmin = this.User.IsInRole(LesGamblers.Common.GlobalConstants.AdministratorRoleName);

            var dataModel = AutoMapper.Mapper.Map<UpdateFinishedGameViewModel, LesGamblers.Models.Game>(model);
            this.games.Add(dataModel);
            this.TempData["Notification"] = "Your game was added successfully! Good luck!";

            return RedirectToAction("Index", "Home");
        }
    }
}