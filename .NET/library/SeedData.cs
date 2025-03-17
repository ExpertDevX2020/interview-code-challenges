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
                Id = Guid.NewGuid(),
                Name = "Ernest Monkjack"
            };
            var sarahKennedy = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Sarah Kennedy"
            };
            var margaretJones = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Margaret Jones"
            };

            var clayBook = new Book
            {
                Id = Guid.NewGuid(),
                Name = "The Importance of Clay",
                Format = BookFormat.Paperback,
                Author = ernestMonkjack,
                ISBN = "1305718181"
            };

            var agileBook = new Book
            {
                Id = Guid.NewGuid(),
                Name = "Agile Project Management - A Primer",
                Format = BookFormat.Hardback,
                Author = sarahKennedy,
                ISBN = "1293910102"
            };

            var rustBook = new Book
            {
                Id = Guid.NewGuid(),
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

            var franckfotso = new Borrower
            {
                Id = Guid.NewGuid(),
                Name = "Franck Fotso",
                EmailAddress = "franck@gmail.com"
            };

            var bookOnLoanUntilToday = new BookStock
            {
                Id = Guid.NewGuid(),
                BookId = clayBook.Id,
                Book = clayBook,
                Borrower = daveSmith,
                BorrowerId = daveSmith.Id,
                LoanEndDate = DateTime.Now.Date
            };

            var bookOnLoanUntilNextWeek = new BookStock
            {
                Id = Guid.NewGuid(),
                BookId = agileBook.Id,
                Book = agileBook,
                Borrower = lianaJames,
                BorrowerId = lianaJames.Id,
                LoanEndDate = DateTime.Now.Date.AddDays(7)
            };

            var BookOnLoanwithOneWeekDelay = new BookStock
            {
                Id = Guid.NewGuid(),
                BookId = rustBook.Id,
                Book = rustBook,
                Borrower = franckfotso,
                BorrowerId = franckfotso.Id,
                LoanEndDate = DateTime.Now.Date.AddDays(-7)
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
                context.Borrowers.Add(franckfotso);

                context.Catalogue.Add(bookOnLoanUntilToday);
                context.Catalogue.Add(bookOnLoanUntilNextWeek);
                context.Catalogue.Add(BookOnLoanwithOneWeekDelay);

                context.SaveChanges();
            }
        }
    }
}
