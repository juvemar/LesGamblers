namespace LesGamblers.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IGamblersService
    {
        IQueryable<Gambler> GetAll();

        IQueryable<Gambler> GetById(string id);

        IQueryable<Gambler> GetByUsername(string username);

        void Add(Gambler gambler);

        void UpdateGambler(Gambler gambler, string id);

        void ChangeGamblerPoints(Gambler gambler, string id);

        void ChangeUserRole(string gamblerId, string role);
    }
}
