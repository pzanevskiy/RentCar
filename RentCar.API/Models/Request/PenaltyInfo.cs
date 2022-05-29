using System;

namespace RentCar.API.Models.Request
{
    public class PenaltyInfo
    {
        public Guid OrderId { get; set; }

        public double ExpirationCost { get; set; }

        public double AdditionalCost { get; set; }

        public string Description { get; set; }
    }
}
