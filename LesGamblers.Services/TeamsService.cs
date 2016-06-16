namespace LesGamblers.Services
{
    using System.Linq;

    using Data;
    using LesGamblers.Services.Contracts;
    using Models;

    public class TeamsService : ITeamsService
    {
        private IRepository<Team> teams;

        public TeamsService(IRepository<Team> teams)
        {
            this.teams = teams;
        }

        public void Add(Team team)
        {
            this.teams.Add(team);
            this.teams.SaveChanges();
        }

        public IQueryable<Team> GetAll()
        {
            return this.teams.All();
        }

        public IQueryable<Team> GetById(int id)
        {
            return this.teams.All().Where(x => x.Id == id).AsQueryable();
        }

        public void AddPlayer(Team team, Player player)
        {
            var currentTeam = this.teams.GetById(team.Id);
            currentTeam.Players.Add(player);
            this.teams.Update(currentTeam);
            this.teams.SaveChanges();
        }
    }
}
