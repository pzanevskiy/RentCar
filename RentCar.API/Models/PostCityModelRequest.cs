using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.API.Models
{
    public class PostCityModelRequest
    {
        public string CityName { get; set; }

        public Guid CountryId { get; set; }
    }
}
