namespace LesGamblers.Web.Models.Gamblers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LesGamblers.Web.Infrastructure;

    public class ListGamblersViewModel : IMapFrom<LesGamblers.Models.Gambler>
    {
        [Required]
        [Display(Name = "Game")]
        public List<SelectListItem> AllGames { get; set; }

        [Required]
        [Display(Name = "Gambler")]
        public List<SelectListItem> AllGamblers { get; set; }
    }
}