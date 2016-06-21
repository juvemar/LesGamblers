namespace LesGamblers.Web.Models.Games
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;

    public class AddGameViewModel : IMapFrom<Game>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date and Time")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Host")]
        public string HostTeam { get; set; }

        [Required]
        [Display(Name = "Guest")]
        public string GuestTeam { get; set; }

        public string FinalResult { get; set; }
        
        public List<SelectListItem> Teams { get; set; }

        public List<SelectListItem> Predictions { get; set; }
    }
}