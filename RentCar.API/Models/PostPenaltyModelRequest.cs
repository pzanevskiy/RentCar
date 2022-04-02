using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.API.Models
{
    public class PostPenaltyModelRequest
    {
        public string Description { get; set; }

        public double PenaltyCost { get; set; }
    }
}
