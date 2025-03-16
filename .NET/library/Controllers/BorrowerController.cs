using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DataAccess;
using OneBeyondApi.DTOs;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowerController : ControllerBase
    {
        private readonly ILogger<BorrowerController> _logger;
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerController(ILogger<BorrowerController> logger, IBorrowerRepository borrowerRepository)
        {
            _logger = logger;
            _borrowerRepository = borrowerRepository;   
        }

        [HttpGet]
        [Route("GetBorrowers")]
        public IList<BorrowerDto> Get()
        {
            return _borrowerRepository.GetBorrowers();
        }

        [HttpPost]
        [Route("AddBorrower")]
        public Guid Post(Borrower borrower)
        {
            return _borrowerRepository.AddBorrower(borrower);
        }

        [HttpGet]
        [Route("Fines")]
        public List<FineDto> GetBorrowerFines(Guid borrowerId)
        {
            using (var context = new LibraryContext())
            {
                return context.Fines
                    .Include(f => f.BookStock)
                    .ThenInclude(bs => bs.Book)
                    .Where(f => f.BorrowerId == borrowerId)
                    .Select(f => new FineDto
                    {
                        BookTitle = f.BookStock.Book.Name,
                        DaysOverdue = f.DaysOverdue,
                        Amount = f.Amount
                    })
                    .ToList();
            }
        }
    }
}