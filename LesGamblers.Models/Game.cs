namespace LesGamblers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game : IDeletableEntity
    {
        private ICollection<Prediction> predictions;
        private ICollection<string> goalscorers;

        public Game()
        {
            this.predictions = new HashSet<Prediction>();
            this.goalscorers = new HashSet<string>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string HostTeam { get; set; }

        [Required]
        public string GuestTeam { get; set; }

        public string FinalResult { get; set; }

        public ICollection<string> Goalscorers
        {
            get { return this.goalscorers; }
            set { this.goalscorers = value; }
        }

        public ICollection<Prediction> Predictions
        {
            get { return this.predictions; }
            set { this.predictions = value; }
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}