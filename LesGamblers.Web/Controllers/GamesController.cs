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
            model.Date = this.FormatDate(model.Date);
            AddGameViewModel newModel = new AddGameViewModel();
            newModel.HostTeam = model.HostTeam;
            newModel.GuestTeam = model.GuestTeam;
            newModel.Date = new DateTime(model.Date.Value.Year, model.Date.Value.Month, model.Date.Value.Day, model.Date.Value.Hour, model.Date.Value.Minute, 0);
            if (newModel.GuestTeam == null || newModel.HostTeam == null || newModel.HostTeam == newModel.GuestTeam)
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
                this.TempData["Notification"] = "The game was not added successfully! Please try again.";
                return this.View(model);
            }

            var dataModel = AutoMapper.Mapper.Map<AddGameViewModel, LesGamblers.Models.Game>(model);
            this.games.Add(dataModel);
            this.TempData["Notification"] = model.HostTeam + " - " + model.GuestTeam + " was added successfully!";

            return RedirectToAction("AddGame", "Games");
        }

        private DateTime FormatDate(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
            {
                return new DateTime();
            }

            var year = dateTime.Value.Year;
            var month = dateTime.Value.Month;
            var day = dateTime.Value.Day;
            var hour = dateTime.Value.Hour;
            var minute = dateTime.Value.Minute;

            return new DateTime(year, month, day, hour, minute, 0);
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
                    Text = game.Date.Value.ToString("dd.MM.yy HH:mm") + "  |  " + game.HostTeam.Replace('_', ' ') + " - " + game.GuestTeam.Replace('_', ' ') + " " + game.FinalResult,
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
                        Text = game.Date.Value.ToString("dd.MM.yy HH:mm") + "  |  " + game.HostTeam.Replace('_', ' ') + " - " + game.GuestTeam.Replace('_', ' ') + " " + game.FinalResult,
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