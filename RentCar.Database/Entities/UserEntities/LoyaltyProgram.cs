using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentCar.Database.Entities.UserEntities
{
    public class LoyaltyProgram
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LoyatyId { get; set; }

        public string LoyaltyName { get; set; }

        [Range(0, 1)]
        public double Discount { get; set; }
    }
}
