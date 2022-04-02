using RentCar.Database.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentCar.Database.Entities.UserEntities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [ForeignKey("AddressId")]
        public Guid? AddressId { get; set; }
        public OrderEntities.Address Address { get; set; }

        public double? Rating { get; set; }

        public LoyaltyProgram Loyalty { get; set; }

        public virtual ICollection<UsersRoles> UsersRoles { get; set; }
    }
}
