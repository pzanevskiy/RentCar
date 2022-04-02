using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.API.Models
{
    public class PostEnhancementModelRequest
    {
        public string Description { get; set; }

        public double Price { get; set; }
    }
}
