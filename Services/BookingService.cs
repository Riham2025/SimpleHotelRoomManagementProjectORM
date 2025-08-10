using System;
using System.Collections.Generic;
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
    }
}
