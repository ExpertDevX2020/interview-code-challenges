namespace OneBeyondApi.DTOs
{
    public class ReturnBookDto
    {
        public Guid? BookStockId { get; set; }
        public Guid? BorrowerId { get; set; }
        public string Title { get; set; }
        public string Borrower { get; set; }
        public DateTime? LoanEndDate { get; set; }
    }
}
