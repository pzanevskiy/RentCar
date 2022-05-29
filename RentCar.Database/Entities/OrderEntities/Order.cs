using RentCar.Database.Entities.CarEntities;
using RentCar.Database.Entities.LocationEntities;
using RentCar.Database.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public DateTime DateTimeCreated { get; set; }

        public DateTime DateTimeFinished { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("CarId")]
        public Guid? CarId { get; set; }
        public virtual Car Car { get; set; }

        [ForeignKey("OrderStatusId")]
        public int? OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        public virtual ICollection<EnhancementsOrders> EnhancementsOrders { get; set; }

        public virtual ICollection<Penalty> Penalties { get; set; }
    }
}
