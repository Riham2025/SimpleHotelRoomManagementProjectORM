using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public interface IBookingService
    {
        void CancelBooking(int bookingId);
        void CreateBooking(int guestId, int roomId, DateTime checkIn, DateTime checkOut);
        List<Booking> GetAllBookings();
        Booking GetBookingById(int bookingId);
        void UpdateBookingDates(int bookingId, DateTime newCheckIn, DateTime newCheckOut);
    }
}