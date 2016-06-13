namespace LesGamblers.Services
{
    using System.Linq;

    using Data;
    using LesGamblers.Services.Contracts;
    using Models;

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

        public void UpdateGame(int id)
        {
            var currentGame = this.games.GetById(id);
            //currentGambler.HealthRecordId = id;

            this.games.Update(currentGame);
            this.games.SaveChanges();
        }
    }
}
