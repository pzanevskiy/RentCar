using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCar.API.Models.Response.Location
{
    public class AddressViewModel
    {
        public Guid AddressId { get; set; }

        public string Addresss { get; set; }
    }
}
