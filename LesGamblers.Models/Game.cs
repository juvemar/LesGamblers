namespace LesGamblers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game : IDeletableEntity
    {
        private ICollection<Prediction> predictions;

        public Game()
        {
            this.predictions = new HashSet<Prediction>();
        }

        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Required]
        public string HostTeam { get; set; }

        [Required]
        public string GuestTeam { get; set; }

        public string FinalResult { get; set; }

        public string Goalscorers { get; set; }

        public ICollection<Prediction> Predictions
        {
            get { return this.predictions; }
            set { this.predictions = value; }
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}