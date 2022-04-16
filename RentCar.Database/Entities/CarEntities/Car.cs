using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Database.Entities.CarEntities
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CarId { get; set; }

        [ForeignKey("CarModelId")]
        public Guid ModelId { get; set; }

        public virtual CarModel Model { get; set; }

        [ForeignKey("CarTypeId")]
        public Guid CarTypeId { get; set; }

        public virtual CarType CarType { get; set; }

        public string Transmission { get; set; }

        public int DoorsCount { get; set; }

        public int SeatsCount { get; set; }

        public bool AC { get; set; }

        public int BagsCount { get; set; }

        public virtual CitiesCars CitiesCars { get; set; }
    }
}
