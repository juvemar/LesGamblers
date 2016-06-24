namespace LesGamblers.Services
{
    using System.Linq;

    using Contracts;
    using Data;
    using Models;

    public class GamblersService : IGamblersService
    {
        private IRepository<Gambler> gamblers;

        public GamblersService(IRepository<Gambler> gamblers)
        {
            this.gamblers = gamblers;
        }

        public void Add(Gambler gambler)
        {
            this.gamblers.Add(gambler);
            this.gamblers.SaveChanges();
        }

        public IQueryable<Gambler> GetAll()
        {
            return this.gamblers.All();
        }

        public Gambler GetById(string id)
        {
            return this.gamblers.GetById(id);
        }

        public IQueryable<Gambler> GetByUsername(string username)
        {
            return this.gamblers.All().Where(u => u.UserName == username).AsQueryable();
        }

        public void UpdateGambler(Gambler gambler, string id)
        {
            var currentGambler = this.gamblers.GetById(id);
            currentGambler.TotalPoints = gambler.TotalPoints == null ? currentGambler.TotalPoints : currentGambler.TotalPoints + gambler.TotalPoints;
            currentGambler.FinalResultsPredicted = gambler.FinalResultsPredicted == null ? currentGambler.FinalResultsPredicted : currentGambler.FinalResultsPredicted + gambler.FinalResultsPredicted;
            currentGambler.GoalscorersPredicted = gambler.GoalscorersPredicted == null ? currentGambler.GoalscorersPredicted : currentGambler.GoalscorersPredicted + gambler.GoalscorersPredicted;
            currentGambler.SignsPredicted = gambler.SignsPredicted == null ? currentGambler.SignsPredicted : currentGambler.SignsPredicted + gambler.SignsPredicted;

            this.gamblers.Update(currentGambler);
            this.gamblers.SaveChanges();
        }
    }
}
