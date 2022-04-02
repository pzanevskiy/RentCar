using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentCar.Database.Entities.OrderEntities
{
    public class Penalty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PenaltyId { get; set; }

        public string Description { get; set; }

        public double PenaltyCost { get; set; }

        public virtual ICollection<PenaltiesOrders> PenaltiesOrders { get; set; }
    }
}
