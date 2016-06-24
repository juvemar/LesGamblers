namespace LesGamblers.Web.Models
{
    using System.Collections.Generic;
    
    using LesGamblers.Models;
    using LesGamblers.Web.Models.Games;

    public class HomeViewModel
    {
        public List<ListAllGamesPredictions> AllGames { get; set; }

        public List<Gambler> AllGamblers { get; set; }
    }
}