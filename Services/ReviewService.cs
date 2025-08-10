using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Models;
using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public class ReviewService 
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

            // 2) Validate rating range (1..5)
            if (rating < 1 || rating > 5)
            {
                error = "Rating must be between 1 and 5.";
                return false;
            }

            // 3) Ensure the guest has at least one completed stay (CheckOut in the past)
            //    Your Booking model should have CheckIn/CheckOut (or CheckInDate/CheckOutDate).
            var hasCompletedStay = _bookingRepo.GetAllBookings()
                .Any(b => b.GuestId == guestId && b.CheckOutDate <= DateTime.Now);

            if (!hasCompletedStay)
            {
                error = "Guest must complete at least one stay before posting a review.";
                return false;
            }

            // 4) Optional anti-spam: block if the guest posted a review too recently
            //    (Since Review model has no CreatedAt, you could add it; for now we can emulate by
            //     assuming the repository can sort by insertion order or you extend the model later.)
            //    If you add a DateTime CreatedAt to Review, use this:
            //    var recent = _reviewRepo.GetReviewsForGuest(guestId)
            //                  .Any(r => r.CreatedAt >= DateTime.Now.Subtract(_antiSpamWindow));
            //    if (recent) { error = "Please wait before posting another review."; return false; }

            // 5) Create entity (trim the comment; allow nulls)
            var review = new Review
            {
                GuestId = guestId,                  // link to the author
                Rating = rating,                   // validated 1..5
                Comment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim()
                // Add CreatedAt = DateTime.Now;  // if you extend the model
            };

            // 6) Persist through repository
            _reviewRepo.AddReview(review);

            // 7) Success
            return true;
        }

        // Update an existing review's rating/comment
        public bool UpdateReview(int reviewId, int newRating, string? newComment, out string error) 
        {
            error = string.Empty; // reset error

            // 1) Load existing review
            var existing = _reviewRepo.GetReviewById(reviewId); // Get the review by ID
            if (existing == null) // Check if review exists
            {
                error = "Review not found.";
                return false;
            }

            // 2) Validate rating range
            if (newRating < 1 || newRating > 5)
            {
                error = "Rating must be between 1 and 5.";
                return false;
            }

            // 3) Apply changes
            existing.Rating = newRating;
            existing.Comment = string.IsNullOrWhiteSpace(newComment) ? null : newComment.Trim();

            // 4) Persist
            _reviewRepo.UpdateReview(existing);

            // 5) Success
            return true;
        }

    }
}
