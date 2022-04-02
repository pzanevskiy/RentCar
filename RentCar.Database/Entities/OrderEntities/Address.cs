using RentCar.Database.Entities.CarEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RentCar.Database.Entities.OrderEntities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderAddressId { get; set; }

        public string OrderAddressName { get; set; }

        [ForeignKey("CityId")]
        public Guid? CityId { get; set; }

        public City City { get; set; }
    }
}
