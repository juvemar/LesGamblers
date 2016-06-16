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
        [StringLength(5, MinimumLength = 2)]
        public string FinalResult { get; set; }

        [Required]
        public string Goalscorer { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int GamblerId { get; set; }

        [UIHint("ChooseGameDropdown")]
        public List<SelectListItem> Games { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AddPredictionViewModel, Prediction>("AddPrediction")
                   .ForMember(m => m.GamblerId, opts => opts.MapFrom(m => m.GamblerId))
                   .ForMember(m => m.GameId, opts => opts.MapFrom(m => m.GameId));
        }
    }
}