using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
