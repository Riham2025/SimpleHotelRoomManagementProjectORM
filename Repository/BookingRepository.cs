using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    // Implements IBookingRepository using EF Core for data access
    public class BookingRepository
    {
        // Private field to hold the database context instance
        private readonly HotelDbContext _context;

        // Constructor that receives the DbContext via dependency injection
        public BookingRepository(HotelDbContext context)
        {
            _context = context; 
        }

    }
}
