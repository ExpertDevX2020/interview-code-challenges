namespace OneBeyondApi.DTOs
{
    public class BookAvailabilityDto
    {
        public Guid BookStockId { get; set; }
        public string BookTitle { get; set; }
        public DateTime? AvailableOn { get; set; }
        public string Message { get; set; }
    }
}
