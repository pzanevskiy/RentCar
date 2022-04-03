using System;

namespace RentCar.API.Models
{
    public class PostCarModelRequest
    {
        public string Model { get; set; }

        public Guid BrandId { get; set; }
    }
}
