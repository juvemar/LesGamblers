namespace LesGamblers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Gambler : IdentityUser, IDeletableEntity
    {
        private ICollection<Prediction> predictions;

        public Gambler()
        {
            this.predictions = new HashSet<Prediction>();
        }
        
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public int TotalPoints { get; set; }

        //public int FinalResultsPredicted { get; set; }

        //public int SignsPredicted { get; set; }

        //public int GoalscorersPredicted { get; set; }

        public virtual ICollection<Prediction> Predictions
        {
            get { return this.predictions; }
            set { this.predictions = value; }
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ClaimsIdentity GenerateUserIdentity(UserManager<Gambler> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Gambler> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }
}
