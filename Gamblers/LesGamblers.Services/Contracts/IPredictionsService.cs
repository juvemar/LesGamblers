namespace LesGamblers.Services.Contracts
{
    using System.Linq;

    using LesGamblers.Models;

    public interface IPredictionsService
    {
        IQueryable<Prediction> GetAll();

        IQueryable<Prediction> GetById(int id);

        void Add(Prediction prediction);

        void UpdatePrediction(Prediction prediction, int id);

        void DeleteAll(bool hardDelete);

        IQueryable<Prediction> GetAllWithDeleted();
    }
}
