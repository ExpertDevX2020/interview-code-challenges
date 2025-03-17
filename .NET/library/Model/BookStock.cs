namespace OneBeyondApi.Model
{
    public class BookStock
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }  
        public DateTime? LoanEndDate { get; set; }
        public Guid? BorrowerId { get; set; }
        public Borrower? Borrower { get; set; }
    }
}
