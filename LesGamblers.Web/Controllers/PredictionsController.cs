﻿namespace LesGamblers.Web.Controllers
{
    using System;
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
        private IPlayersService players;

        public PredictionsController(IGamblersService gamblers, IPredictionsService predictions, IGamesService games, IPlayersService players)
        {
            this.gamblers = gamblers;
            this.predictions = predictions;
            this.games = games;
            this.players = players;
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddPrediction(AddPredictionViewModel model)
        {
            var timeNow = DateTime.Now.AddHours(1);
            var availableGames = this.games
                                .GetAll()
                                .Where(g => string.IsNullOrEmpty(g.FinalResult))// && g.Date > timeNow
                                //.OrderBy(g => g.Date)
                                .ToList();

            model.Games = new List<SelectListItem>();
            foreach (var game in availableGames)
            {
                model.Games.Add(new SelectListItem
                {
                    Text = "  |  " + game.HostTeam.Replace('_', ' ') + " - " + game.GuestTeam.Replace('_', ' '),//game.Date.ToString("dd.MM.yy HH:mm") + 
                    Value = game.Id.ToString()
                });
            }
            //model.Players = new List<SelectListItem>();

            return this.View(model);
        }

        [Authorize]
        [ActionName("AddPrediction")]
        [HttpPost]
        public ActionResult AddPredictionPost(AddPredictionViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            model.FinalResult = model.FinalResult.Trim();

            var dataModel = AutoMapper.Mapper.Map<AddPredictionViewModel, LesGamblers.Models.Prediction>(model);

            var currentGambler = this.gamblers.GetByUsername(this.User.Identity.Name).FirstOrDefault();
            dataModel.Gambler = currentGambler;

            var alreadyPredictedMatch = this.predictions.GetAll()
                .Where(p => p.GamblerId == currentGambler.Id && p.GameId.ToString() == model.GameId)
                .ToList();
            if (alreadyPredictedMatch.Count() > 0)
            {
                this.predictions.UpdatePrediction(dataModel, alreadyPredictedMatch.FirstOrDefault().Id);
                this.TempData["Notification"] = "Your prediction was updated successfully! Good luck!";
            }
            else
            {
                this.predictions.Add(dataModel);
                this.TempData["Notification"] = "Your prediction was added successfully! Good luck!";
            }
            
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [ActionName("_PlayersDropdownPartial")]
        [HttpGet]
        public JsonResult GetPlayersForGoalscorer(string gameId)
        {
            var game = this.games.GetById(int.Parse(gameId)).FirstOrDefault();
            var gameHost = game.HostTeam.Replace('_', ' ');
            var gameGuest = game.GuestTeam.Replace('_', ' ');
            var firstTeamPlayers = this.players.GetAll()
                .Where(x => x.Country == gameHost).Select(a => new
                {
                    Name = a.Name,
                    Country = a.Country,
                    Club = a.ClubTeam
                })
                .ToList();
            var secondTeamPlayers = this.players.GetAll()
                .Where(x => x.Country == gameGuest).Select(a => new
                {
                    Name = a.Name,
                    Country = a.Country,
                    Club = a.ClubTeam
                })
                .ToList();
            return Json(new { hostPlayers = firstTeamPlayers, guestPlayers = secondTeamPlayers }, JsonRequestBehavior.AllowGet);
        }
    }
}