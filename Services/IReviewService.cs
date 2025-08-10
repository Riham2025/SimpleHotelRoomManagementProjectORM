using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public interface IReviewService // High-level contract for Review operations
    {
        bool AddReview(int guestId, int rating, string? comment, out string error); // Create a new review with validations
        bool DeleteReview(int reviewId, out string error); // Delete a review by ID (may add business rules later, e.g., cannot delete if too old)
        List<Review> GetAllReviews(); // Return all reviews
        double GetAverageRatingForGuest(int guestId); // Compute average rating for a guest
        List<Review> GetReviewsForGuest(int guestId); // Return reviews for a specific guest
        bool UpdateReview(int reviewId, int newRating, string? newComment, out string error); // Update an existing review with validations (rating range, comment length, etc.)
    }
}