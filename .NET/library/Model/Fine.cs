namespace OneBeyondApi.Model
{
    public class Fine
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public Guid? BookStockId { get; set; }
        public BookStock BookStock { get; set; }
        public int DaysOverdue { get; set; }
        public decimal Amount { get; set; }
    }
}
