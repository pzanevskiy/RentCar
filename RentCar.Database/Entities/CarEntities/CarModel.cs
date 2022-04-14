using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.CarEntities
{
    public class CarModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ModelId { get; set; }

        public string ModelName { get; set; }

        [ForeignKey("BrandId")]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
