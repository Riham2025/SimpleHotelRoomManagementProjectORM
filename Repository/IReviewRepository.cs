using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    public interface IReviewRepository // Interface for Review repository
    {
        void AddReview(Review review); // Add a new review to the database
        void DeleteReview(int id); // Delete a review by its ID
        List<Review> GetAllReviews(); // Retrieve all reviews
        double GetAverageRatingForGuest(int guestId); // Calculate average rating for a guest
        Review GetReviewById(int id); // Retrieve a review by its ID
        List<Review> GetReviewsForGuest(int guestId); // Retrieve reviews for a specific guest
        void UpdateReview(Review review); // Update an existing review in the database
    }
}