using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.API.Models
{
    public class PostCarModelRequest
    {
        public string Model { get; set; }

        public Guid BrandId { get; set; }
    }
}
