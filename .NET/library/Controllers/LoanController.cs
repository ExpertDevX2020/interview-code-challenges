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

        public LoanController(ILogger<BookController> logger, IBorrowerRepository borrowerRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;
        }

        [HttpGet]
        [Route("GetOnLoan")]
        public IEnumerable<BorrowerWithLoansDto> GetActiveLoans()
        {
            return _borrowerRepository.GetBorrowersWithActiveLoans();
        }
    }
}
