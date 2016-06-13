namespace LesGamblers.Services.Contracts
{
    using Models;
    using System.Linq;

    public interface IGamesService
    {
        IQueryable<Game> GetAll();

        IQueryable<Game> GetById(int id);

        void Add(Game game);

        void UpdateGame(int id);
    }
}
