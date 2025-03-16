namespace OneBeyondApi.DTOs
{
    public class BorrowerWithLoansDto
    {
        public string BorrowerName { get; set; }
        public string Email { get; set; }
        public List<BookOnLoanDto> BooksOnLoan { get; set; }
    }
}
