namespace LesGamblers.Services
{
    using System.Linq;

    using Data;
    using Contracts;
    using Models;

    public class PlayersService : IPlayersService
    {
        private IRepository<Player> players;

        public PlayersService(IRepository<Player> players)
        {
            this.players = players;
        }

        public void Add(Player player)
        {
            this.players.Add(player);
            this.players.SaveChanges();
        }

        public IQueryable<Player> GetAll()
        {
            return this.players.All();
        }

        public IQueryable<Player> GetAllWithDeleted()
        {
            return this.players.AllWithDeleted();
        }

        public IQueryable<Player> GetById(int id)
        {
            return this.players.All().Where(x => x.Id == id).AsQueryable();
        }

        public void DeleteAll(bool hardDelete)
        {
            var allPlayers = this.GetAllWithDeleted().ToList();

            foreach (var player in allPlayers)
            {
                if (hardDelete)
                {
                    this.players.Delete(player);
                }
                else
                {
                    this.players.MarkAsDeleted(player);
                }
            }
            this.players.SaveChanges();
        }

    }
}
