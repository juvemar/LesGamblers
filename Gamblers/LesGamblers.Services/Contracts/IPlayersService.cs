namespace LesGamblers.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IPlayersService
    {
        IQueryable<Player> GetAll();

        IQueryable<Player> GetById(int id);

        void Add(Player player);

        void DeleteAll(bool hardDelete);

        IQueryable<Player> GetAllWithDeleted();
    }
}
