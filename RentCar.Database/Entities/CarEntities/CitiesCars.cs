using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.CarEntities
{
    public class CitiesCars
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CitiesCarId { get; set; }

        [ForeignKey("CityId")]
        public Guid CityId { get; set; }

        [ForeignKey("CarId")]
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }

        public double Price { get; set; }
    }
}
