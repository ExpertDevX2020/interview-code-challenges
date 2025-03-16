using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DTOs;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class CatalogueRepository : ICatalogueRepository
    {
        public CatalogueRepository()
        {
        }

        public IEnumerable<BookStockDto> GetCatalogue()
        {
            using (var context = new LibraryContext())
            {
                return context.Catalogue
                    .Select(bs => new BookStockDto
                    {
                        Id = bs.Id,
                        Title = bs.Book.Name,
                        Author = bs.Book.Author.Name,
                        LoanEndDate = bs.LoanEndDate,
                        BorrowerName = bs.Borrower.Name
                    })
                    .ToList();
            }
        }

        public List<BookStockDto> SearchCatalogue(CatalogueSearch search)
        {
            using (var context = new LibraryContext())
            {
                var query = context.Catalogue
                    .Include(x => x.Book)
                    .ThenInclude(x => x.Author)
                    .Include(x => x.Borrower)
                    .AsQueryable();

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.Author))
                    {
                        query = query.Where(x => x.Book.Author.Name.Contains(search.Author));
                    }
                    if (!string.IsNullOrEmpty(search.BookName))
                    {
                        query = query.Where(x => x.Book.Name.Contains(search.BookName));
                    }
                }

                return query.Select(bs => new BookStockDto
                {
                    Id = bs.Id,
                    Title = bs.Book.Name,
                    Author = bs.Book.Author.Name,
                    LoanEndDate = bs.LoanEndDate,
                    BorrowerName = bs.Borrower.Name
                }).ToList();
            }
        }
    }
}
