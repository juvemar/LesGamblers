namespace LesGamblers.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteEntitiesViewModel
    {
        [Display(Name = "Delete all Games")]
        public bool DeleteGames { get; set; }

        [Display(Name = "Delete all Predictions")]
        public bool DeletePredictions { get; set; }

        [Display(Name = "Delete all Players")]
        public bool DeletePlayers { get; set; }

        [Display(Name = "Delete all Teams")]
        public bool DeleteTeams { get; set; }

        [Display(Name = "Hard Delete")]
        public bool HardDelete { get; set; }
    }
}