namespace LesGamblers.Web.Models.Gamblers
{
    using LesGamblers.Web.Infrastructure;
    using System.ComponentModel.DataAnnotations;

    public class ListGamblersViewModel : IMapFrom<LesGamblers.Models.Gambler>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Total Points")]
        public int TotalPoints { get; set; }

        [Display(Name = "Exactly Predicted Results")]
        public int FinalResultsPredicted { get; set; }

        [Display(Name = "Signs Predicted")]
        public int SignsPredicted { get; set; }

        [Display(Name = "Goalscorers Predicted")]
        public int GoalscorersPredicted { get; set; }
    }
}