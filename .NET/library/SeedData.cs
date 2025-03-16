using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi
{
    public class SeedData
    {
        public static void SetInitialData()
        {
            var ernestMonkjack = new Author
            {
                Name = "Ernest Monkjack"
            };
            var sarahKennedy = new Author
            {
                Name = "Sarah Kennedy"
            };
            var margaretJones = new Author
            {
                Name = "Margaret Jones"
            };

            var clayBook = new Book
            {
                Name = "The Importance of Clay",
                Format = BookFormat.Paperback,
                Author = ernestMonkjack,
                ISBN = "1305718181"
            };

            var agileBook = new Book
            {
                Name = "Agile Project Management - A Primer",
                Format = BookFormat.Hardback,
                Author = sarahKennedy,
                ISBN = "1293910102"
            };

            var rustBook = new Book
            {
                Name = "Rust Development Cookbook",
                Format = BookFormat.Paperback,
                Author = margaretJones,
                ISBN = "3134324111"
            };

            var daveSmith = new Borrower
            {
                Id = Guid.NewGuid(),
                Name = "Dave Smith",
                EmailAddress = "dave@smithy.com"
            };

            var lianaJames = new Borrower
            {
                Id = Guid.NewGuid(),
                Name = "Liana James",
                EmailAddress = "liana@gmail.com"
            };

            var bookOnLoanUntilToday = new BookStock
            {
                Id = Guid.NewGuid(),
                Book = clayBook,
                Borrower = daveSmith,
                BorrowerId = daveSmith.Id,
                LoanEndDate = DateTime.Now.Date
            };

            var bookNotOnLoan = new BookStock
            {
                Id = Guid.NewGuid(),
                Book = clayBook,
                Borrower = null,
                BorrowerId = null,
                LoanEndDate = null
            };

            var bookOnLoanUntilNextWeek = new BookStock
            {
                Id = Guid.NewGuid(),
                Book = agileBook,
                Borrower = lianaJames,
                BorrowerId = lianaJames.Id,
                LoanEndDate = DateTime.Now.Date.AddDays(-7)
            };

            var rustBookStock = new BookStock
            {
                Id = Guid.NewGuid(),
                Book = rustBook,
                Borrower = null,
                BorrowerId = null,
                LoanEndDate = null
            };

            using (var context = new LibraryContext())
            {
                context.Authors.Add(ernestMonkjack);
                context.Authors.Add(sarahKennedy);
                context.Authors.Add(margaretJones);

                context.Books.Add(clayBook);
                context.Books.Add(agileBook);
                context.Books.Add(rustBook);

                context.Borrowers.Add(daveSmith);
                context.Borrowers.Add(lianaJames);

                context.Catalogue.Add(bookOnLoanUntilToday);
                context.Catalogue.Add(bookNotOnLoan);
                context.Catalogue.Add(bookOnLoanUntilNextWeek);
                context.Catalogue.Add(rustBookStock);

                context.SaveChanges();
            }
        }
    }
}
