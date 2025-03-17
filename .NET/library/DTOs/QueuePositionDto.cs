namespace OneBeyondApi.DTOs
{
    public class QueuePositionDto
    {
        public Guid BookStockId { get; set; }
        public Guid BorrowerId { get; set; }
        public int Position { get; set; }
    }
}
