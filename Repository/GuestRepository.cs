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
            // Find the first guest where GuestId equals the provided id; returns null if none
            return _context.Guests.FirstOrDefault(g => g.GuestId == id);
        }

        // Retrieve a guest by email (unique or first match)
        public Guest GetGuestByEmail(string email)
        {
            
            return _context.Guests.FirstOrDefault(g => g.Email == email); // Get the first guest with the specified email
        }

        // Add a new guest to the database
        public void AddGuest(Guest guest)
        {
            // Stage the new entity for insertion
            _context.Guests.Add(guest);
            // Commit the pending change to the database (INSERT)
            _context.SaveChanges();
        }

        // Update an existing guest
        public void UpdateGuest(Guest guest)
        {
            // Mark the guest as modified (EF will generate UPDATE for changed fields)
            _context.Guests.Update(guest);
            // Persist the update to the database
            _context.SaveChanges();
        }

    }
}
