namespace LesGamblers.Services
{
    using System.Linq;

    using Data;
    using LesGamblers.Services.Contracts;
    using Models;

    public class PredictionsService : IPredictionsService
    {
        private IRepository<Prediction> predictions;

        public PredictionsService(IRepository<Prediction> predictions)
        {
            this.predictions = predictions;
        }

        public void Add(Prediction prediction)
        {
            this.predictions.Add(prediction);
            this.predictions.SaveChanges();
        }

        public IQueryable<Prediction> GetAll()
        {
            return this.predictions.All();
        }

        public IQueryable<Prediction> GetById(int id)
        {
            return this.predictions.All().Where(x => x.Id == id).AsQueryable();
        }
    }
}
