namespace LesGamblers.Web.Models.Predictions
{
    using System.ComponentModel.DataAnnotations;

    using LesGamblers.Web.Infrastructure;

    public class AddPredictionViewModel : IMapFrom<LesGamblers.Models.Prediction>
    {
        [Required]
        [StringLength(5, MinimumLength = 2)]
        public string FinalResult { get; set; }

        [Required]
        public string Goalscorer { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int GamblerId { get; set; }
    }
}