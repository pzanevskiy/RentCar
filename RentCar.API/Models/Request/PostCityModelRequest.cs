using System;

namespace RentCar.API.Models.Request
{
    public class PostCityModelRequest
    {
        public string CityName { get; set; }

        public Guid CountryId { get; set; }
    }
}
