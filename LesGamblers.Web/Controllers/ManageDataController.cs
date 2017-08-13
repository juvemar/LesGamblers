using System.Web.Mvc;

using LesGamblers.Services.Contracts;
using LesGamblers.Web.Models;

namespace LesGamblers.Web.Controllers
{
    public class ManageDataController : Controller
    {
        private IGamesService games;
        private IPredictionsService predictions;
        private IPlayersService players;
        private ITeamsService teams;

        public ManageDataController(IGamesService games,
                                    IPredictionsService predictions,
                                    IPlayersService players,
                                    ITeamsService teams)
        {
            this.games = games;
            this.predictions = predictions;
            this.players = players;
            this.teams = teams;
        }

        [HttpGet]
        [Authorize(Roles = LesGamblers.Common.GlobalConstants.AdministratorRoleName)]
        public ActionResult DeleteEntities()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult DeleteEntities(DeleteEntitiesViewModel model)
        {
            if (model.DeleteGames)
            {
                this.games.DeleteAll(model.HardDelete);
            }

            if (model.DeletePredictions)
            {
                this.predictions.DeleteAll(model.HardDelete);
            }

            if (model.DeletePlayers)
            {
                this.players.DeleteAll(model.HardDelete);
            }

            if (model.DeleteTeams)
            {
                this.players.DeleteAll(model.HardDelete);
                this.teams.DeleteAll(model.HardDelete);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}