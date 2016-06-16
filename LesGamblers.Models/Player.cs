namespace LesGamblers.Models
{
    using System;

    public class Player : IDeletableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ClubTeam { get; set; }

        public int Number { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
