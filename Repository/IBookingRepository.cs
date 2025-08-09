using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    public interface IBookingRepository // Interface for Booking repository
    {
        void AddBooking(Booking booking); // Add a new booking to the database
        void DeleteBooking(int id); // Delete a booking by its ID
        List<Booking> GetActiveBookingsForRoom(int roomId, DateTime? onDate = null); // Retrieve active bookings for a specific room, optionally on a specific date
        List<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        List<Booking> GetBookingsByGuest(int guestId);
        void UpdateBooking(Booking booking);
    }
}