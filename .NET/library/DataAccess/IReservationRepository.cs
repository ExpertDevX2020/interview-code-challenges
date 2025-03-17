using OneBeyondApi.DTOs;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IReservationRepository
    {
        public ReservationDto ReserveBook(Guid bookStockId);

        public List<ReservationDto> GetReservationsForBook(Guid bookStockId);

        public BookAvailabilityDto GetBookAvailabilityDate(Guid bookStockId);

        public void AssignNextReservation(Guid bookStockId);

        public int? GetReservationPosition(Guid bookStockId, Guid borrowerId);
    }
}
