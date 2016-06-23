namespace LesGamblers.Web.Models.Games
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;

    public class UpdateFinishedGameViewModel : IMapFrom<Game>, IHaveCustomMappings
    {
        [Display(Name = "Game")]
        public int Id { get; set; }

        [Display(Name = "Final Result")]
        [Required]
        public string FinalResult { get; set; }

        [Display(Name = "Goalscorers")]
        public string GoalscorersList { get; set; }

        public List<string> Goalscorers { get; set; }

        public List<SelectListItem> Games { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<UpdateFinishedGameViewModel, Game>("UpdateGame")
                   .ForMember(m => m.Goalscorers, opts => opts.MapFrom(m => m.Goalscorers));
        }
    }
}