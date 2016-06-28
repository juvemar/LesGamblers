namespace LesGamblers.Web.Models.Games
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    
    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;
    
    public class ListAllGamesPredictions : IMapFrom<Game>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string HostTeam { get; set; }

        public string GuestTeam { get; set; }

        [Display(Name = "Final Result")]
        public string FinalResult { get; set; }

        public ICollection<Prediction> Predictions { get; set; }

        public ICollection<Gambler> Gamblers { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Game, ListAllGamesPredictions>("ListAllGames")
                   .ForMember(m => m.Predictions, opts => opts.MapFrom(m => m.Predictions));
        }
    }
}