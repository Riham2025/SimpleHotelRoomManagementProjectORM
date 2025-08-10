using SimpleHotelRoomManagementProjectORM.Repository;
using SimpleHotelRoomManagementProjectORM.Services;

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


            RoomService roomService = new RoomService(roomRepo);// Room service (business logic for rooms)
            GuestService guestService = new GuestService(guestRepo); // Guest service (business logic for guests)
            BookingService bookingService = new BookingService(bookingRepo, roomRepo, guestRepo); // Booking service (business logic for bookings)
            ReviewService reviewService = new ReviewService(reviewRepo, bookingRepo, guestRepo);// Review service (business logic for reviews)

            // ===================== SIMPLE INTERACTIVE MENU =====================


        }
    }
}
