namespace OneBeyondApi.DTOs
{
    public class BorrowerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public List<BookStockDto> BooksOnLoan { get; set; } = new List<BookStockDto>();
    }
}
