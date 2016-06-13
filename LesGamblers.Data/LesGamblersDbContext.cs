namespace LesGamblers.Data
{
    using Models;
    using System.Data.Entity;

    public class LesGamblersDbContext : DbContext
    {
        public LesGamblersDbContext()
            :base("LesGamblers")
        {
        }

        public virtual IDbSet<Game> Games { get; set; }

        public virtual IDbSet<Prediction> Predictions { get; set; }

        public virtual IDbSet<Gambler> Gamblers { get; set; }

        public static LesGamblersDbContext Create()
        {
            return new LesGamblersDbContext();
        }
    }
}
