using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public  class ReviewService
    {

        // Reference to review data-access
        private readonly IReviewRepository _reviewRepo;

        // We also need bookings to validate "guest has stayed before"
        private readonly IBookingRepository _bookingRepo;// To access bookings for reviews

        // guests to validate existence
        private readonly IGuestRepository _guestRepo; // To access guests for reviews

        // (e.g., do not allow more than one review per 24h per guest)
        private readonly TimeSpan _antiSpamWindow = TimeSpan.FromHours(24);

        // Constructor injection for dependencies (DI-friendly)
        public ReviewService(IReviewRepository reviewRepo,
                             IBookingRepository bookingRepo,
                             IGuestRepository guestRepo)
        {
            _reviewRepo = reviewRepo;   // store review repository
            _bookingRepo = bookingRepo;  // store booking repository
            _guestRepo = guestRepo;    
        }
    }
}
