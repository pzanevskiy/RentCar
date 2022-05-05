using System;
using System.Collections.Generic;

namespace RentCar.API.Models.Request
{
    public class PostOrderRequest
    {
        public Guid CarId { get; set; }

        public Guid CityId { get; set; }

        public IEnumerable<Guid> Enhancements { get; set; }

        public Guid PickUpAddressId { get; set; }

        public Guid ReturnAddressId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
