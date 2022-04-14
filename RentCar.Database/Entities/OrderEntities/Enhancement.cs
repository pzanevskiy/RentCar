using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.OrderEntities
{
    public class Enhancement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EnhancementId { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public virtual ICollection<EnhancementsOrders> EnhancementsOrders { get; set; }
    }
}
