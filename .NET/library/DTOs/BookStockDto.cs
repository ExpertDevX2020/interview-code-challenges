namespace OneBeyondApi.DTOs
{
    public class BookStockDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? LoanEndDate { get; set; }
        public string BorrowerName { get; set; }
    }
}
