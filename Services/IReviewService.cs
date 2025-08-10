using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public interface IReviewService // High-level contract for Review operations
    {
        bool AddReview(int guestId, int rating, string? comment, out string error);
        bool DeleteReview(int reviewId, out string error);
        List<Review> GetAllReviews();
        double GetAverageRatingForGuest(int guestId);
        List<Review> GetReviewsForGuest(int guestId);
        bool UpdateReview(int reviewId, int newRating, string? newComment, out string error);
    }
}