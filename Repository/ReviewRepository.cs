using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    public class ReviewRepository
    {

        // Reference to database context
        private readonly HotelDbContext _context;

        // Constructor with dependency injection
        public ReviewRepository(HotelDbContext context)
        {
            _context = context; // Store the context for use in methods
        }
    }
}
