namespace LesGamblers.Web.Models.Gamblers
{
    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;

    public class UpdateGamblerViewModel : IMapFrom<Gambler>, IHaveCustomMappings
    {
        public int TotalPoints { get; set; }

        public int FinalResultsPredicted { get; set; }

        public int SignsPredicted { get; set; }

        public int GoalscorersPredicted { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<UpdateGamblerViewModel, Gambler>("UpdateGamblerPoints");
        }
    }
}