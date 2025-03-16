using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Helper;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BookRepository : IBookRepository
    {
        public BookRepository()
        {
        }
        public List<Book> GetBooks()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Books
                    .Include(bs => bs.Author)
                    .ToList();
                return list;
            }
        }

        public Guid AddBook(Book book)
        {
            using (var context = new LibraryContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
                return book.Id;
            }
        }

        public BookStock ReturnBook(Guid bookStockId)
        {
            using (var context = new LibraryContext())
            {
                var bookStock = context.Catalogue
                    .Include(bs => bs.Book) 
                    .Include(bs => bs.Borrower)
                    .FirstOrDefault(bs => bs.Id == bookStockId);

                if (bookStock == null)
                {
                    throw new Exception("Book not found or not on loan.");
                }

                if (bookStock.LoanEndDate.HasValue && bookStock.LoanEndDate.Value < DateTime.UtcNow)
                {
                    bool fineExists = context.Fines.Any(f => f.BookStockId == bookStockId);
                    if (!fineExists && bookStock.BorrowerId != null)
                    {
                        int daysOverdue = (DateTime.UtcNow - bookStock.LoanEndDate.Value).Days;
                        decimal fineAmount = FineCalculator.CalculateFine(bookStock.LoanEndDate.Value);

                        var borrower = context.Borrowers.FirstOrDefault(b => b.Id == bookStock.BorrowerId);
                        var bookStockEntry = context.Catalogue.FirstOrDefault(bs => bs.Id == bookStockId);

                        var fine = new Fine
                        {
                            Id = Guid.NewGuid(),
                            BorrowerId = bookStock.BorrowerId,
                            Borrower = bookStock.Borrower,
                            BookStockId = bookStockId,
                            BookStock = bookStock,
                            DaysOverdue = daysOverdue,
                            Amount = fineAmount
                        };

                        context.Fines.Add(fine);
                    }
                }

                context.SaveChanges();

                return bookStock;
            }
        }
    }
}
