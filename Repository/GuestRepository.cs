using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProjectORM.Repository
{

    // Concrete repository that implements IGuestRepository using EF Core
    public class GuestRepository
    {

        // Private field holding the injected DbContext instance
        private readonly HotelDbContext _context;
    }
}
