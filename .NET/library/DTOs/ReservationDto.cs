namespace OneBeyondApi.DTOs
{
    public class ReservationDto
    {
        public Guid BorrowerId { get; set; }
        public Guid BookStockId { get; set; }
        public DateTime ReservedAt { get; set; }
        public int QueuePosition { get; set; }
    }
}
