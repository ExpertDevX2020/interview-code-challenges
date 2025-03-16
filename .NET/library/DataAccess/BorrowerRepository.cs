using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DTOs;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BorrowerRepository : IBorrowerRepository
    {
        public BorrowerRepository()
        {
        }

        public List<BorrowerDto> GetBorrowers()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Borrowers
                    .Include(b => b.BookStocks)
                        .ThenInclude(bs => bs.Book)
                            .ThenInclude(b => b.Author)
                    .Select(b => new BorrowerDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        EmailAddress = b.EmailAddress,
                        BooksOnLoan = b.BookStocks.Select(bs => new BookStockDto
                        {
                            Id = bs.Id,
                            Title = bs.Book.Name,
                            Author = bs.Book.Author.Name,
                            LoanEndDate = bs.LoanEndDate,
                            BorrowerName = b.Name
                        }).ToList()
                    })
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
