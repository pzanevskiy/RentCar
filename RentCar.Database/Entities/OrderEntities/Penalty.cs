using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.OrderEntities
{
    public class Penalty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PenaltyId { get; set; }

        public string Description { get; set; }

        public double PenaltyCost { get; set; }

        [ForeignKey("OrderId")]
        public Guid? OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
