using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.DTOs;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IBookRepository _bookRepository;

        public LoanController(ILogger<BookController> logger, IBorrowerRepository borrowerRepository, IBookRepository bookRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Route("GetOnLoan")]
        public IEnumerable<BorrowerWithLoansDto> GetActiveLoans()
        {
            return _borrowerRepository.GetBorrowersWithActiveLoans();
        }

        [HttpPost]
        [Route("ReturnBook")]
        public ReturnBookDto ReturnBook([FromBody] Guid bookStockId)
        {
            var updatedBookStock = _bookRepository.ReturnBook(bookStockId);

            return new ReturnBookDto
            {
                BookStockId = updatedBookStock.Id,
                BorrowerId = updatedBookStock.BorrowerId,
                Title = updatedBookStock.Book.Name,
                Borrower = updatedBookStock.Borrower.Name,
                LoanEndDate = updatedBookStock.LoanEndDate
            };
        }
    }
}
