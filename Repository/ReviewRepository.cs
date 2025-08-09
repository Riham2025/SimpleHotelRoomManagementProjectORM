using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleHotelRoomManagementProjectORM.Models;

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

        // Get all reviews
        public List<Review> GetAllReviews()
        {
            // Return all reviews with Guest info
            return _context.Reviews
                           .Include(r => r.Guest) // Load the associated Guestc
                           .ToList(); // Execute query and return list

        }

        // Get review by ID
        public Review GetReviewById(int id)
        {
            return _context.Reviews // Load the associated Guest
                           .Include(r => r.Guest)
                           .FirstOrDefault(r => r.ReviewId == id); // Execute query and return the first match or null
        }

        // Get reviews for a specific guest
        public List<Review> GetReviewsForGuest(int guestId)
        {
            return _context.Reviews // Load the associated Guest
                           .Where(r => r.GuestId == guestId) // Filter by GuestId
                           .ToList(); // Execute query and return list
        }

        // Calculate average rating for a guest
        public double GetAverageRatingForGuest(int guestId)
        {
            var query = _context.Reviews.Where(r => r.GuestId == guestId);// Filter reviews by GuestId

            // Return 0 if no reviews found
            if (!query.Any())
                return 0.0;

            // Calculate average rating
            return query.Average(r => r.Rating);

        }

        // Add a new review
        public void AddReview(Review review)
        {
            _context.Reviews.Add(review); // Add the review to the context
            _context.SaveChanges(); // Save changes to the database
        }

        // Update an existing review
        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review); // Update the review in the context
            _context.SaveChanges(); // Save changes to the database
        }

        // Delete a review by ID
        public void DeleteReview(int id)
        {
            var existing = _context.Reviews.FirstOrDefault(r => r.ReviewId == id); // Find the review by ID
            if (existing != null) // Check if the review exists
            {
                _context.Reviews.Remove(existing); // Mark the review for deletion
                _context.SaveChanges();
            }
        }
    }
}
