using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    public interface IReviewRepository // Interface for Review repository
    {
        void AddReview(Review review); // Add a new review to the database
        void DeleteReview(int id); // Delete a review by its ID
        List<Review> GetAllReviews();
        double GetAverageRatingForGuest(int guestId);
        Review GetReviewById(int id);
        List<Review> GetReviewsForGuest(int guestId);
        void UpdateReview(Review review);
    }
}