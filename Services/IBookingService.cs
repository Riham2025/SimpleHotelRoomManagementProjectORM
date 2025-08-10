using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public interface IBookingService // Interface for booking operations
    {
        void CancelBooking(int bookingId); // Cancel a booking by ID
        void CreateBooking(int guestId, int roomId, DateTime checkIn, DateTime checkOut);
        List<Booking> GetAllBookings();
        Booking GetBookingById(int bookingId);
        void UpdateBookingDates(int bookingId, DateTime newCheckIn, DateTime newCheckOut);
    }
}