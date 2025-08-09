using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    public interface IReviewRepository
    {
        void AddReview(Review review);
        void DeleteReview(int id);
        List<Review> GetAllReviews();
        double GetAverageRatingForGuest(int guestId);
        Review GetReviewById(int id);
        List<Review> GetReviewsForGuest(int guestId);
        void UpdateReview(Review review);
    }
}