using OneBeyondApi.DTOs;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BorrowerRepository : IBorrowerRepository
    {
        public BorrowerRepository()
        {
        }
        public List<Borrower> GetBorrowers()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Borrowers
                    .ToList();
                return list;
            }
        }

        public Guid AddBorrower(Borrower borrower)
        {
            using (var context = new LibraryContext())
            {
                context.Borrowers.Add(borrower);
                context.SaveChanges();
                return borrower.Id;
            }
        }

        public IEnumerable<BorrowerWithLoansDto> GetBorrowersWithActiveLoans()
        {
            using (var context = new LibraryContext())
            {
                return context.Borrowers
                .Where(b => b.BookStocks.Any(bs => bs.LoanEndDate != null))
                .Select(b => new BorrowerWithLoansDto
                {
                    BorrowerName = b.Name,
                    Email = b.EmailAddress,
                    BooksOnLoan = b.BookStocks
                        .Where(bs => bs.LoanEndDate != null)
                        .Select(bs => new BookOnLoanDto
                        {
                            Title = bs.Book.Name,
                            LoanEndDate = bs.LoanEndDate
                        }).ToList()
                }).ToList();
            }
        }
    }
}
