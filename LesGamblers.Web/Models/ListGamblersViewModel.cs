namespace LesGamblers.Web.Models
{
    using LesGamblers.Web.Infrastructure;

    public class ListGamblersViewModel : IMapFrom<LesGamblers.Models.Gambler>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalPoints { get; set; }
    }
}