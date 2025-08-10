using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Models;
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
            _guestRepo = guestRepo;  // store guest repository
        }

        // Create a new review with business validations
        public bool AddReview(int guestId, int rating, string? comment, out string error)
        {
            error = string.Empty; // initialize error

            // 1) Validate guest existence
            var guest = _guestRepo.GetGuestById(guestId);
            if (guest == null)
            {
                error = "Guest not found.";
                return false; // fail fast
            }

            // 
            if (rating < 1 || rating > 5)
            {
                error = "Rating must be between 1 and 5.";
                return false;
            }

            // 
            //    Your Booking model should have CheckIn/CheckOut (or CheckInDate/CheckOutDate).
            var hasCompletedStay = _bookingRepo.GetAllBookings()
                .Any(b => b.GuestId == guestId && b.CheckOut <= DateTime.Now);

            if (!hasCompletedStay)
            {
                error = "Guest must complete at least one stay before posting a review.";
                return false;
            }

            // 
            //    (Since Review model has no CreatedAt, you could add it; for now we can emulate by
            //     assuming the repository can sort by insertion order or you extend the model later.)
            //    If you add a DateTime CreatedAt to Review, use this:
            //    var recent = _reviewRepo.GetReviewsForGuest(guestId)
            //                  .Any(r => r.CreatedAt >= DateTime.Now.Subtract(_antiSpamWindow));
            //    if (recent) { error = "Please wait before posting another review."; return false; }

            
            var review = new Review
            {
                GuestId = guestId,                  // 
                Rating = rating,                   // 
                Comment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim()
                
            };

           
            _reviewRepo.AddReview(review);

          
            return true;
        }
    }
}
