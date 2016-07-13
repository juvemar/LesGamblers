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

        public void UpdatePrediction(Prediction prediction, int id)
        {
            var currentPrediction = this.predictions.GetById(id);
            currentPrediction.FinalResult = prediction.FinalResult == null ? currentPrediction.FinalResult : prediction.FinalResult;
            currentPrediction.Goalscorer = prediction.Goalscorer == null ? currentPrediction.Goalscorer : prediction.Goalscorer;
            currentPrediction.TotalPoints = prediction.TotalPoints;
            currentPrediction.GoalscorerPredicted = prediction.GoalscorerPredicted;
            currentPrediction.SignPredicted = prediction.SignPredicted;
            currentPrediction.FinalResultPredicted = prediction.FinalResultPredicted;

            this.predictions.Update(currentPrediction);
            this.predictions.SaveChanges();
        }
    }
}
