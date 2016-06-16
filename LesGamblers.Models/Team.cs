namespace LesGamblers.Models
{
    using System;
    using System.Collections.Generic;

    public class Team : IDeletableEntity
    {
        private ICollection<Player> players;

        public Team()
        {
            this.players = new HashSet<Player>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Player> Players
        {
            get { return this.players; }
            set { this.players = value; }
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
