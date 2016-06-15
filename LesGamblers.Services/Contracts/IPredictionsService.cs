namespace LesGamblers.Services.Contracts
{
    using System.Linq;

    using LesGamblers.Models;

    public interface IPredictionsService
    {
        IQueryable<Prediction> GetAll();

        IQueryable<Prediction> GetById(int id);

        void Add(Prediction prediction);
    }
}
