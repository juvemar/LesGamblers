namespace LesGamblers.Services.Contracts
{
    using System.Linq;

    using LesGamblers.Models;

    public interface ITeamsService
    {
        IQueryable<Team> GetAll();

        IQueryable<Team> GetById(int id);

        void Add(Team team);

        void AddPlayer(Team team, Player player);
    }
}
