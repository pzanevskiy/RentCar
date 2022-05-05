using RentCar.Database.Entities.CarEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.LocationEntities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CityId { get; set; }

        public string CityName { get; set; }

        [ForeignKey("CountryId")]
        public Guid CountryId { get; set; }
     
        public virtual Country Country { get; set; }

        public virtual ICollection<CitiesCars> Cars { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
