using RentCar.API.Models.Response.Car;
using System.Collections.Generic;

namespace RentCar.API.Models.Response
{
    public class GetCarsResponse
    {
        public IEnumerable<CarPriceViewModel> Cars { get; set; }
    }
}
