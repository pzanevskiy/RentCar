namespace RentCar.API.Models.Request
{
    public class PostPenaltyModelRequest
    {
        public string Description { get; set; }

        public double PenaltyCost { get; set; }
    }
}
