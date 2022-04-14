using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.LocationEntities
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
