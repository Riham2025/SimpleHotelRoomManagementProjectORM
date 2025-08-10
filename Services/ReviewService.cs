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

        private readonly IGuestRepository _guestRepo; // To access guests for reviews

    }
}
