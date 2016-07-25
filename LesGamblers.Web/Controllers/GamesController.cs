namespace LesGamblers.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Helper;
    using LesGamblers.Web.Models.Games;
    using LesGamblers.Web.Models.Gamblers;
    using LesGamblers.Models;

    public class GamesController : Controller
    {
        private ITeamsService teams;
        private IGamesService games;
        private IPlayersService players;
        private IPredictionsService predictions;
        private IGamblersService gamblers;

        public GamesController(ITeamsService teams, IGamesService games, IPlayersService players, IPredictionsService predictions, IGamblersService gamblers)
        {
            this.teams = teams;
            this.games = games;
            this.players = players;
            this.predictions = predictions;
            this.gamblers = gamblers;
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
            if (model.GuestTeam == null || model.HostTeam == null || model.HostTeam == model.GuestTeam ||
                DateTime.Now > new DateTime(model.Date.Year, model.Date.Month, model.Date.Day, model.Date.Hour, model.Date.Minute, model.Date.Second))
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

            var dataModel = AutoMapper.Mapper.Map<AddGameViewModel, LesGamblers.Models.Game>(model);
            this.games.Add(dataModel);
            this.TempData["Notification"] = model.HostTeam + " - " + model.GuestTeam + " was added successfully!";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
        public ActionResult UpdateFinishedGame(UpdateFinishedGameViewModel model)
        {
            var allTeams = this.games.GetAll()
                            .Where(g => g.Date < DateTime.Now)
                            .ToList()
                            .OrderBy(g => g.Date);

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
                var allTeams = this.games.GetAll()
                            .Where(g => g.Date < DateTime.Now)
                            .ToList()
                            .OrderBy(g => g.Date);

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

            var dataModel = AutoMapper.Mapper.Map<UpdateFinishedGameViewModel, LesGamblers.Models.Game>(model);

            this.games.UpdateGame(dataModel, model.Id);
            PointsUpdater.CheckCorrectPredictions(model, this.predictions, this.gamblers);

            this.TempData["Notification"] = "The game was updated successfully!";

            return RedirectToAction("UpdateFinishedGame", "Games");
        }
    }
}