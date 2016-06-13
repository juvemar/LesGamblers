namespace LesGamblers.Services.Contracts
{
    using Models;
    using System.Linq;

    public interface IPredictionsService
    {
        IQueryable<Prediction> GetAll();

        IQueryable<Prediction> GetById(int id);

        void Add(Prediction prediction);
    }
}
