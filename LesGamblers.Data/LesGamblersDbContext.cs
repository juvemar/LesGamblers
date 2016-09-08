namespace LesGamblers.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using LesGamblers.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;
    public class LesGamblersDbContext : IdentityDbContext<Gambler>, ILesGamblersDbContext
    {
        public LesGamblersDbContext()
            : base("LesGamblers")
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Game>()
            .Property(f => f.Date)
            .HasColumnType("datetime2");

            base.OnModelCreating(modelBuilder);
        }
    }
}
