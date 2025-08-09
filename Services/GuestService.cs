using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Models;
using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public class GuestService
    {
        // Reference to the data-access layer for guests
        private readonly IGuestRepository _guestRepo;

        // Inject the repository through the constructor (DI-friendly)
        public GuestService(IGuestRepository guestRepo)
        {
            _guestRepo = guestRepo; // Store the repository for later use
        }

        // Get all guests (no extra rules needed here)
        public List<Guest> GetAllGuests() 
        {
            return _guestRepo.GetAllGuests(); // Delegate to repository
        }

        // Get a guest by ID (pass-through)
        public Guest GetGuestById(int id)
        { }

    }
}
