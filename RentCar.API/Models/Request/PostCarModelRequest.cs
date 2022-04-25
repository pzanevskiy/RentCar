using System;

namespace RentCar.API.Models.Request
{
    public class PostCarModelRequest
    {
        public string Model { get; set; }

        public Guid BrandId { get; set; }
    }
}
