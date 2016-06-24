namespace LesGamblers.Web.Models.Games
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    
    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;
    using System.ComponentModel.DataAnnotations;
    
    public class ListAllGamesPredictions : IMapFrom<Game>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Game")]
        public string Rivals { get; set; }

        [Display(Name = "Final Result")]
        public string FinalResult { get; set; }

        public ICollection<Prediction> Predictions { get; set; }

        public ICollection<Gambler> Gamblers { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ListAllGamesPredictions, Game>("ListAllPredictions")
                   .ForMember(m => m.HostTeam + " - " + m.GuestTeam, opts => opts.MapFrom(m => m.Rivals));
        }
    }
}