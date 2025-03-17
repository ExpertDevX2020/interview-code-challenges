using Microsoft.EntityFrameworkCore;
using OneBeyondApi.DTOs;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class ReservationRepository : IReservationRepository
    {
        public ReservationRepository()
        {
        }

        public ReservationDto ReserveBook(Guid bookStockId)
        {
            using (var context = new LibraryContext())
            {
                var bookStock = context.Catalogue
                    .Include(bs => bs.Book)
                    .FirstOrDefault(bs => bs.Id == bookStockId);

                if (bookStock == null)
                    throw new Exception("Book not found.");

                if (!bookStock.LoanEndDate.HasValue)
                    throw new Exception("Book is currently available. No reservation needed.");

                int queueCount = context.Reservations.Count(r => r.BookStockId == bookStockId);

                var reservation = new Reservation
                {
                    Id = Guid.NewGuid(),
                    BorrowerId = (Guid)bookStock.BorrowerId,
                    BookStockId = bookStockId,
                    ReservedAt = DateTime.UtcNow,
                    QueuePosition = queueCount + 1
                };

                context.Reservations.Add(reservation);
                context.SaveChanges();

                return new ReservationDto
                {
                    BorrowerId = (Guid)bookStock.BorrowerId,
                    BookStockId = bookStockId,
                    ReservedAt = reservation.ReservedAt,
                    QueuePosition = reservation.QueuePosition
                };
            }
        }

        public List<ReservationDto> GetReservationsForBook(Guid bookStockId)
        {
            using (var context = new LibraryContext())
            {
                var reservations = context.Reservations
                    .Where(r => r.BookStockId == bookStockId)
                    .OrderBy(r => r.ReservedAt)
                    .Select((r, index) => new ReservationDto
                    {
                        BorrowerId = r.BorrowerId,
                        BookStockId = r.BookStockId,
                        ReservedAt = r.ReservedAt,
                        QueuePosition = index + 1
                    })
                    .ToList();

                return reservations;
            }
        }

        public void AssignNextReservation(Guid bookStockId)
        {
            using (var context = new LibraryContext())
            {
                var nextReservation = context.Reservations
                    .Where(r => r.BookStockId == bookStockId)
                    .OrderBy(r => r.ReservedAt)
                    .FirstOrDefault();

                if (nextReservation != null)
                {
                    var bookStock = context.Catalogue.Find(bookStockId);
                    bookStock.BorrowerId = nextReservation.BorrowerId;
                    bookStock.LoanEndDate = DateTime.UtcNow.AddDays(7); // One week default loan time

                    context.Reservations.Remove(nextReservation);

                    var remainingReservations = context.Reservations
                        .Where(r => r.BookStockId == bookStockId)
                        .OrderBy(r => r.QueuePosition)
                        .ToList();

                    for (int i = 0; i < remainingReservations.Count; i++)
                    {
                        remainingReservations[i].QueuePosition = i + 1;
                    }

                    context.SaveChanges();
                }
            }
        }

        public BookAvailabilityDto GetBookAvailabilityDate(Guid bookStockId)
        {
            using (var context = new LibraryContext())
            {
                var bookStock = context.Catalogue
                    .Include(bs => bs.Book)
                    .Include(bs => bs.Borrower)
                    .FirstOrDefault(bs => bs.Id == bookStockId);

                if (bookStock == null)
                    throw new Exception("Book not found.");

                if (bookStock.LoanEndDate.HasValue && bookStock.LoanEndDate.Value < DateTime.UtcNow)
                {
                    return new BookAvailabilityDto
                    {
                        BookStockId = bookStockId,
                        BookTitle = bookStock.Book?.Name,
                        AvailableOn = null,
                        Message = "Book return is delayed. Availability date is uncertain."
                    };
                }

                return new BookAvailabilityDto
                {
                    BookStockId = bookStockId,
                    BookTitle = bookStock.Book?.Name,
                    AvailableOn = bookStock.LoanEndDate,
                    Message = bookStock.LoanEndDate.HasValue
                        ? $"Expected availability date: {bookStock.LoanEndDate.Value}"
                        : "Book is currently available."
                };
            }
        }

        public int? GetReservationPosition(Guid bookStockId, Guid borrowerId)
        {
            using (var context = new LibraryContext())
            {
                var reservation = context.Reservations
                    .Where(r => r.BookStockId == bookStockId && r.BorrowerId == borrowerId)
                    .Select(r => new { r.QueuePosition })
                    .FirstOrDefault();

                return reservation?.QueuePosition;
            }
        }
    }
}
