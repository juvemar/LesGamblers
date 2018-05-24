namespace LesGamblers.Models
{
    using System;
    using System.Collections.Generic;

    public class Leage
    {
        private ICollection<Gambler> gamblers;

        public Leage()
        {
            this.gamblers = new HashSet<Gambler>();
            this.Code = new Guid();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public Guid Code { get; set; }

        public virtual ICollection<Gambler> Gamblers
        {
            get { return this.gamblers; }
            set { this.gamblers = value; }
        }
    }
}
