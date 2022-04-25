namespace RentCar.API.Models.Request
{
    public class PostCarRequest
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public string Transmission { get; set; }

        public int DoorsCount { get; set; }

        public int SeatsCount { get; set; }

        public bool AC { get; set; }

        public int BagsCount { get; set; }
    }
}
