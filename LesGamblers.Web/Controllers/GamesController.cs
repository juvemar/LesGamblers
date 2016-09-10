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
    using System.Globalization;

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
            model.Date = model.Date.ToUniversalTime();
            //model.Date = this.FormatDate(model.Date);

            if (model.GuestTeam == null || model.HostTeam == null || model.HostTeam == model.GuestTeam)
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
            //dataModel.Date = this.FormatDate(model.Date);
            this.games.Add(dataModel);
            this.TempData["Notification"] = model.HostTeam + " - " + model.GuestTeam + " was added successfully!";

            return RedirectToAction("AddGame", "Games");
        }

        private DateTime FormatDate(DateTime dateTime)
        {
            var day = 1;
            switch (dateTime.Day)
            {
                case 1: day = 1; break;
                case 2: day = 2; break;
                case 3: day = 3; break;
                case 4: day = 4; break;
                case 5: day = 5; break;
                case 6: day = 6; break;
                case 7: day = 7; break;
                case 8: day = 8; break;
                case 9: day = 9; break;
                case 10: day = 10; break;
                case 11: day = 11; break;
                case 12: day = 12; break;
                case 13: day = 13; break;
                case 14: day = 14; break;
                case 15: day = 15; break;
                case 16: day = 16; break;
                case 17: day = 17; break;
                case 18: day = 18; break;
                case 19: day = 19; break;
                case 20: day = 20; break;
                case 21: day = 21; break;
                case 22: day = 22; break;
                case 23: day = 23; break;
                case 24: day = 24; break;
                case 25: day = 25; break;
                case 26: day = 26; break;
                case 27: day = 27; break;
                case 28: day = 28; break;
                case 29: day = 29; break;
                case 30: day = 30; break;
                case 31: day = 31; break;
                default: day = 1;  break;
            }
            var month = 1;
            switch (dateTime.Day)
            {
                case 1: month = 1; break;
                case 2: month = 2; break;
                case 3: month = 3; break;
                case 4: month = 4; break;
                case 5: month = 5; break;
                case 6: month = 6; break;
                case 7: month = 7; break;
                case 8: month = 8; break;
                case 9: month = 9; break;
                case 10: month = 10; break;
                case 11: month = 11; break;
                case 12: month = 12; break;
                default: day = 1; break;
            }

            return new DateTime(dateTime.Year, month, day, dateTime.Hour, dateTime.Minute, 0);
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