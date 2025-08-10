using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public interface IBookingService // Interface for booking operations
    {
        void CancelBooking(int bookingId); // Cancel a booking by ID
        void CreateBooking(int guestId, int roomId, DateTime checkIn, DateTime checkOut); // Create a new booking
        List<Booking> GetAllBookings(); // Get all bookings
        Booking GetBookingById(int bookingId);// Get a booking by ID
        void UpdateBookingDates(int bookingId, DateTime newCheckIn, DateTime newCheckOut);
    }
}