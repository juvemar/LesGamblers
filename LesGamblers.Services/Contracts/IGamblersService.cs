namespace LesGamblers.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IGamblersService
    {
        IQueryable<Gambler> GetAll();

        Gambler GetById(string id);

        IQueryable<Gambler> GetByUsername(string username);

        void Add(Gambler gambler);

        void UpdateGambler(int id);
    }
}
