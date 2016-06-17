namespace LesGamblers.Web.Models.Gamblers
{
    using LesGamblers.Web.Infrastructure;

    public class ListGamblersViewModel : IMapFrom<LesGamblers.Models.Gambler>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalPoints { get; set; }

        public int FinalResultsPredicted { get; set; }

        public int SignsPredicted { get; set; }

        public int GoalscorersPredicted { get; set; }
    }
}