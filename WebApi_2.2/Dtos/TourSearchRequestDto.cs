namespace WebApi_2._2.Dtos
{
    public class TourSearchRequestDto
    {
        public decimal MinPrice { get; set; } = decimal.MinValue;
        public decimal MaxPrice { get; set; } = decimal.MaxValue;
    }
}