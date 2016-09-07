namespace LesGamblers.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Player : IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string SecondTeam { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string CurrentTeam { get; set; }
        
        public int TeamId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}
