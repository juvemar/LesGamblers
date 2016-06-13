namespace LesGamblers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Gambler : IDeletableEntity
    {
        private ICollection<Prediction> predictions;

        public Gambler()
        {
            this.predictions = new HashSet<Prediction>();
            this.TotalPoints = 0;
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TotalPoints { get; set; }

        public virtual ICollection<Prediction> Predictions
        {
            get { return this.predictions; }
            set { this.predictions = value; }
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
