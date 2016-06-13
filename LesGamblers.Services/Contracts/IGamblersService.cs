namespace LesGamblers.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IGamblersService
    {
        IQueryable<Gambler> GetAll();

        IQueryable<Gambler> GetById(int id);

        void Add(Gambler gambler);

        void UpdateGambler(int id);
    }
}
