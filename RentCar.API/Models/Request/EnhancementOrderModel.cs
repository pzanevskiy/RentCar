using System;

namespace RentCar.API.Models.Request
{
    public class EnhancementOrderModel
    {
        public Guid EnhancementId { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
