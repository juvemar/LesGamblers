namespace LesGamblers.Services
{
    using System.Linq;

    using Contracts;
    using Data;
    using Models;
    using Microsoft.AspNet.Identity;

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

        public IQueryable<Gambler> GetById(string id)
        {
            return this.gamblers.All().Where(x => x.Id == id).AsQueryable(); ;
        }

        public IQueryable<Gambler> GetByUsername(string username)
        {
            return this.gamblers.All().Where(u => u.UserName == username).AsQueryable();
        }

        public void UpdateGambler(Gambler gambler, string id)
        {
            var currentGambler = this.gamblers.GetById(id);
            currentGambler.TotalPoints = gambler.TotalPoints == 0 ? currentGambler.TotalPoints : currentGambler.TotalPoints + gambler.TotalPoints;
            currentGambler.FinalResultsPredicted = gambler.FinalResultsPredicted == 0 ? currentGambler.FinalResultsPredicted : currentGambler.FinalResultsPredicted + gambler.FinalResultsPredicted;
            currentGambler.GoalscorersPredicted = gambler.GoalscorersPredicted == 0 ? currentGambler.GoalscorersPredicted : currentGambler.GoalscorersPredicted + gambler.GoalscorersPredicted;
            currentGambler.SignsPredicted = gambler.SignsPredicted == 0 ? currentGambler.SignsPredicted : currentGambler.SignsPredicted + gambler.SignsPredicted;

            this.gamblers.Update(currentGambler);
            this.gamblers.SaveChanges();
        }

        public void ChangeGamblerPoints(Gambler gambler, string id)
        {
            var currentGambler = this.gamblers.GetById(id);
            currentGambler.TotalPoints = gambler.TotalPoints == 0 ? currentGambler.TotalPoints : gambler.TotalPoints;
            currentGambler.FinalResultsPredicted = gambler.FinalResultsPredicted == 0 ? currentGambler.FinalResultsPredicted : gambler.FinalResultsPredicted;
            currentGambler.GoalscorersPredicted = gambler.GoalscorersPredicted == 0 ? currentGambler.GoalscorersPredicted : gambler.GoalscorersPredicted;
            currentGambler.SignsPredicted = gambler.SignsPredicted == 0 ? currentGambler.SignsPredicted : gambler.SignsPredicted;

            this.gamblers.Update(currentGambler);
            this.gamblers.SaveChanges();
        }

        public void ChangeUserRole(string gamblerId, string role)
        {
            this.gamblers.ChangeUserRole(gamblerId, role);
            this.gamblers.SaveChanges();
        }
    }
}
