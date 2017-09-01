namespace LesGamblers.Web.Models.Predictions
{
    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;

    public class CheckPredictionDetailsViewModel : IMapFrom<Prediction>
    {
        public string FinalResult { get; set; }

        public string Goalscorer { get; set; }

        public string ActualResult { get; set; }

        public string ActualGoalscorers { get; set; }

        public int TotalPoints { get; set; }
    }
}