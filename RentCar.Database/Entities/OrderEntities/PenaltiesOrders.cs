using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentCar.Database.Entities.OrderEntities
{
    public class PenaltiesOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PenaltiesOrdersId { get; set; }

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("PenaltyId")]
        public Guid PenaltyId { get; set; }
        public virtual Penalty Penalty { get; set; }
    }
}
