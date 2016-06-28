namespace LesGamblers.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LesGamblers.Models;
    using LesGamblers.Web.Models.Games;
    using LesGamblers.Web.Models.Gamblers;

    public class HomeViewModel
    {
        [Display(Name = "Game")]
        public List<SelectListItem> AllGames { get; set; }

        [Display(Name = "Gambler")]
        public List<SelectListItem> AllGamblers { get; set; }
    }
}