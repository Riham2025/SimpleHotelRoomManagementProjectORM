using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public class BookingService
    {

        private readonly IBookingRepository _bookingRepository;  // To interact with booking data storage
        private readonly IRoomRepository _roomRepository; // To interact with room data
        private readonly IGuestRepository _guestRepository; // To interact with guest data

        // Constructor for dependency injection
        public BookingService(IBookingRepository bookingRepo, IRoomRepository roomRepo, IGuestRepository guestRepo)
        {
            _bookingRepository = bookingRepo; // Store the booking repository
            _roomRepository = roomRepo; // Store the room repository
            _guestRepository = guestRepo; // Store the guest repository
        }

        // Create a new booking
        public void CreateBooking(int guestId, int roomId, DateTime checkIn, DateTime checkOut)
        {
            // Validate guest exists
            var guest = _guestRepository.GetById(guestId);
            if (guest == null)
                throw new Exception("Guest not found.");

        }
    }
}
