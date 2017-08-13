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

        public IQueryable<Team> GetAllWithDeleted()
        {
            return this.teams.AllWithDeleted();
        }

        public IQueryable<Team> GetById(int id)
        {
            return this.teams.All().Where(x => x.Id == id).AsQueryable();
        }

        public void DeleteAll(bool hardDelete)
        {
            var allTeams = this.GetAllWithDeleted().ToList();

            foreach (var team in allTeams)
            {
                if (hardDelete)
                {
                    this.teams.Delete(team);
                }
                else
                {
                    this.teams.MarkAsDeleted(team);
                }
            }
            this.teams.SaveChanges();
        }
    }
}
