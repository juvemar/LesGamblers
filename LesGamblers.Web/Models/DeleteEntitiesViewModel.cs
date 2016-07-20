namespace LesGamblers.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteEntitiesViewModel
    {
        [Display(Name = "Delete all Games")]
        public bool DeleteGames { get; set; }

        [Display(Name = "Delete all Predictions")]
        public bool DeletePredictions { get; set; }
    }
}