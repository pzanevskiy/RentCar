using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.API.Models.Response.Location
{
    public class CityViewModel
    {
        public Guid CityId { get; set; }

        public string CityName { get; set; }

        public IEnumerable<AddressViewModel> Addresses { get; set; }
    }
}
