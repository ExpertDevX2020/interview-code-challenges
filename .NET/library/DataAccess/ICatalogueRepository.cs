using OneBeyondApi.DTOs;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface ICatalogueRepository
    {
        public IEnumerable<BookStockDto> GetCatalogue();

        public List<BookStockDto> SearchCatalogue(CatalogueSearch search);
    }
}
