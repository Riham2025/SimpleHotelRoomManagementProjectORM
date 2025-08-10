using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ===================== INITIALIZATION (DbContext + Repositories + Services) =====================

            HotelDbContext dbContext = new HotelDbContext();// Create the database context

            RoomRepository roomRepo = new RoomRepository(dbContext); // Room repository
            GuestRepository guestRepo = new GuestRepository(dbContext);// Guest repository
            BookingRepository bookingRepo = new BookingRepository(dbContext);// Booking repository
            ReviewRepository reviewRepo = new ReviewRepository(dbContext); // Review repository   



        }
    }
}
