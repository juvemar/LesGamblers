namespace LesGamblers.Web.Models.Predictions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;

    public class AddPredictionViewModel : IMapFrom<Prediction>, IHaveCustomMappings
    {
        [Required]
        [RegularExpression("^[0-9]+[-: ][0-9]+$", ErrorMessage = "Final Result can contain only digits, ':' or '-'")]
        [Display(Name = "Final Result")]
        public string FinalResult { get; set; }

        [Display(Name = "Goalscorer")]
        public string Goalscorer { get; set; }

        [Display(Name = "Choose Game")]
        public string GameId { get; set; }

        public int GamblerId { get; set; }

        public List<SelectListItem> Games { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AddPredictionViewModel, Prediction>("AddPrediction")
                   .ForMember(m => m.GamblerId, opts => opts.MapFrom(m => m.GamblerId))
                   .ForMember(m => m.GameId, opts => opts.MapFrom(m => m.GameId));
        }
    }
}