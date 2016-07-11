namespace LesGamblers.Services
{
    using System.Linq;

    using Data;
    using LesGamblers.Services.Contracts;
    using Models;
    using System.Collections.Generic;

    public class GamesService : IGamesService
    {
        private IRepository<Game> games;

        public GamesService(IRepository<Game> games)
        {
            this.games = games;
        }

        public void Add(Game game)
        {
            this.games.Add(game);
            this.games.SaveChanges();
        }

        public IQueryable<Game> GetAll()
        {
            return this.games.All();
        }

        public IQueryable<Game> GetById(int id)
        {
            return this.games.All().Where(x => x.Id == id).AsQueryable();
        }

        public void UpdateGame(Game game, int id)
        {
            var currentGame = this.games.GetById(id);
            currentGame.FinalResult = game.FinalResult == null ? currentGame.FinalResult : game.FinalResult;
            currentGame.Goalscorers = game.Goalscorers == null ? currentGame.Goalscorers : game.Goalscorers;

            this.games.Update(currentGame);
            this.games.SaveChanges();
        }
    }
}
