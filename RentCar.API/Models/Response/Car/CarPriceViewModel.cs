using System;

namespace RentCar.API.Models.Response.Car
{
    public class CarPriceViewModel
    {
        public Guid CarId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public string Transmission { get; set; }

        public int DoorsCount { get; set; }

        public int SeatsCount { get; set; }

        public bool AC { get; set; }

        public int BagsCount { get; set; }

        public double Price { get; set; }

        public string PictureLink { get; set; }
    }
}
