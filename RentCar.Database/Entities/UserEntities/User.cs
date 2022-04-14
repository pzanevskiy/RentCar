using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.UserEntities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [ForeignKey("LoyaltyProgramId")]
        public Guid? LoyaltyProgramId { get; set; }

        public virtual LoyaltyProgram LoyaltyProgram { get; set; }

        public virtual IList<UsersRoles> UsersRoles { get; set; }
    }
}
