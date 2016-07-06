namespace LesGamblers.Web.Models.Gamblers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using LesGamblers.Models;
    using LesGamblers.Web.Infrastructure;

    public class UpdateGamblerViewModel : IMapFrom<Gambler>, IHaveCustomMappings
    {
        [Display(Name = "Gambler")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Total Points")]
        public int TotalPoints { get; set; }

        [Display(Name = "Exactly Predicted Results")]
        public int FinalResultsPredicted { get; set; }

        [Display(Name = "Signs Predicted")]
        public int SignsPredicted { get; set; }

        [Display(Name = "Goalscorers Predicted")]
        public int GoalscorersPredicted { get; set; }

        [Display(Name = "Make Admin")]
        public bool MakeAdmin { get; set; }

        public List<SelectListItem> AllGamblers { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<UpdateGamblerViewModel, Gambler>("UpdateGamblerPoints");
        }
    }
}