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
        [Display(Name="Game")]
        public int Id { get; set; }

        [Display(Name = "Final Result")]
        public string FinalResult { get; set; }

        public string Goalscorers { get; set; }

        public List<SelectListItem> Games { get; set; }

        public List<SelectListItem> GoalscorersList { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AddGameViewModel, Game>("AddGame");
        }
    }
}