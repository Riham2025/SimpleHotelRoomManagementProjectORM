using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{

    // Concrete repository that implements IGuestRepository using EF Core
    public class GuestRepository
    {

        // Private field holding the injected DbContext instance
        private readonly HotelDbContext _context;

        // Constructor receives DbContext via dependency injection
        public GuestRepository(HotelDbContext context)
        {
            // Store the incoming context for later use in methods
            _context = context;
        }

        // Retrieve all guests from the database
        public List<Guest> GetAllGuests()
        {
            
            return _context.Guests.ToList(); // Get all guests from the Guests table
        }

        // Retrieve a guest by its primary key (GuestId)
        public Guest GetGuestById(int id)
        {
           
            return _context.Guests.FirstOrDefault(g => g.GuestId == id);
        }

    }
}
