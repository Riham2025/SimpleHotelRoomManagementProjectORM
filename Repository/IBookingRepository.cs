using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    public interface IBookingRepository // Interface for Booking repository
    {
        void AddBooking(Booking booking); // Add a new booking to the database
        void DeleteBooking(int id);
        List<Booking> GetActiveBookingsForRoom(int roomId, DateTime? onDate = null);
        List<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        List<Booking> GetBookingsByGuest(int guestId);
        void UpdateBooking(Booking booking);
    }
}