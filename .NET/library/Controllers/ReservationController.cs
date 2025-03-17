using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.DTOs;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(ILogger<BookController> logger, IReservationRepository reservationRepository)
        {
            _logger = logger;
            _reservationRepository = reservationRepository;
        }

        [HttpPost]
        [Route("Reserve")]
        public ReservationDto ReserveBook([FromBody] Guid bookStockId)
        {
            _reservationRepository.ReserveBook(bookStockId);

            return new ReservationDto
            {
                BookStockId = bookStockId,
                ReservedAt = DateTime.UtcNow
            };
        }

        [HttpGet]
        [Route("Availability")]
        public BookAvailabilityDto GetBookAvailability(Guid bookStockId)
        {
            var availabilityInfo = _reservationRepository.GetBookAvailabilityDate(bookStockId);

            if (availabilityInfo == null)
            {
                throw new Exception("Book not found.");
            }

            return new BookAvailabilityDto
            {
                BookStockId = bookStockId,
                BookTitle = availabilityInfo.BookTitle,
                AvailableOn = availabilityInfo.AvailableOn,
                Message = availabilityInfo.Message
            };
        }

        [HttpGet]
        [Route("QueuePosition")]
        public QueuePositionDto GetQueuePosition(Guid bookStockId, Guid borrowerId)
        {
            var position = _reservationRepository.GetReservationPosition(bookStockId, borrowerId);

            return new QueuePositionDto
            {
                BookStockId = bookStockId,
                BorrowerId = borrowerId,
                Position = position.Value
            };
        }

    }
}
