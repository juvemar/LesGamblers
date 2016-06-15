﻿namespace LesGamblers.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Prediction : IDeletableEntity
    {
        public int Id { get; set; }

        [Required]
        public string FinalResult { get; set; }

        [Required]
        public string Goalscorer { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int GamblerId { get; set; }

        public virtual Gambler Gambler { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}