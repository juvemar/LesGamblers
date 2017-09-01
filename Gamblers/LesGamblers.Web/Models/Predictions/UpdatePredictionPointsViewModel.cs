namespace LesGamblers.Web.Models.Predictions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;

    public class UpdatePredictionPointsViewModel : IMapFrom<Prediction>, IHaveCustomMappings
    {
        public string FinalResult { get; set; }

        public int TotalPoints { get; set; }

        public bool FinalResultPredicted { get; set; }

        public bool SignPredicted { get; set; }

        public bool GoalscorerPredicted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<UpdatePredictionPointsViewModel, Prediction>("UpdatePrediction");
        }
    }
}