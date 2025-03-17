namespace OneBeyondApi.Model
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public Guid BookStockId { get; set; }
        public BookStock BookStock { get; set; }
        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
        public int QueuePosition { get; set; }
    }
}
