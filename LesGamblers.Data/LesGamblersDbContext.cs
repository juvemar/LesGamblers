namespace LesGamblers.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using LesGamblers.Models;

    public class LesGamblersDbContext : IdentityDbContext<Gambler>, ILesGamblersDbContext
    {
        public LesGamblersDbContext()
            :base("LesGamblers")
        {
        }

        public virtual IDbSet<Game> Games { get; set; }

        public virtual IDbSet<Prediction> Predictions { get; set; }

        public virtual IDbSet<Team> Teams { get; set; }

        public virtual IDbSet<Player> Players { get; set; }

        public override IDbSet<Gambler> Users { get; set; }

        public static LesGamblersDbContext Create()
        {
            return new LesGamblersDbContext();
        }
    }
}
