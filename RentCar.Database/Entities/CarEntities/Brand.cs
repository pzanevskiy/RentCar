using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.CarEntities
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BrandId { get; set; }

        public string BrandName { get; set; }

        public virtual ICollection<CarModel> Models { get; set; }
    }
}
