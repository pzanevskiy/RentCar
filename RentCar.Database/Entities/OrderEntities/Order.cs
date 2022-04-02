using RentCar.Database.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentCar.Database.Entities.OrderEntities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        [ForeignKey("PickUpAddressId")]
        public Guid PickUpAddressId { get; set; }
        public virtual Address PickUpAddress { get; set; }

        [ForeignKey("ReturnAddressId")]
        public Guid ReturnAddressId { get; set; }
        public virtual Address ReturnAddress { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public double TotalAmount { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("DriverId")]
        public Guid? DriverId { get; set; }
        public virtual User Driver { get; set; }

        [ForeignKey("OrderStatusId")]
        public int? OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual ICollection<EnhancementsOrders> EnhancementsOrders { get; set; }

        public virtual ICollection<PenaltiesOrders> PenaltiesOrders { get; set; }
    }
}
