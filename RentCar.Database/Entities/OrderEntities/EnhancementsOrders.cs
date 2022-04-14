using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.OrderEntities
{
    public class EnhancementsOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EnhancementsOrdersId { get; set; }

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("EnhancementId")]
        public Guid EnhancementId { get; set; }
        public virtual Enhancement Enhancement { get; set; }
    }
}
