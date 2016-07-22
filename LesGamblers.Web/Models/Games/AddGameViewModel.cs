namespace LesGamblers.Web.Models.Games
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;

    public class AddGameViewModel : IMapFrom<Game>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        //[Required]
        public DateTime Date { get; set; }

        [Display(Name = "Host")]
        //[Required]
        public string HostTeam { get; set; }

        [Display(Name = "Guest")]
        //[Required]
        public string GuestTeam { get; set; }

        public List<SelectListItem> Teams { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<AddGameViewModel, Game>("AddGame");
        }
    }
}