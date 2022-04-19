using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.API.Models.Response.Location
{
    public class CountryViewModel
    {
        public Guid CountryId { get; set; }

        public string CountryName { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
