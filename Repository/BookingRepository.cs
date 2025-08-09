using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    // Implements IBookingRepository using EF Core for data access
    public class BookingRepository
    {
        // Private field to hold the database context instance
        private readonly HotelDbContext _context;

        // Constructor that receives the DbContext via dependency injection
        public BookingRepository(HotelDbContext context)
        {
            _context = context; // Store the context for use in methods
        }

        // Retrieve all bookings from the database
        public List<Booking> GetAllBookings()
        {
            // Return all bookings, including related Room and Guest data
            return _context.Bookings
                           .Include(b => b.Room)   // Load the associated Room
                           .Include(b => b.Guest)  // Load the associated Guest
                           .ToList();              // Execute query and return list
        }

        // Retrieve a booking by its ID
        public Booking GetBookingById(int id)
        {
            // Return the first booking that matches the given ID, including relationships
            return _context.Bookings
                           .Include(b => b.Room) // Load the associated Room
                           .Include(b => b.Guest) // Load the associated Guest
                           .FirstOrDefault(b => b.BookingId == id); // Execute query and return the first match or null
        }

        // Retrieve all bookings for a specific guest
        public List<Booking> GetBookingsByGuest(int guestId)
        {
            // Return bookings where GuestId matches, including Room info
            return _context.Bookings
                           .Include(b => b.Room) // Load the associated Room
                           .Where(b => b.GuestId == guestId) // Filter by GuestId
                           .ToList(); // Execute query and return list
        }

        // Retrieve active bookings for a specific room
        public List<Booking> GetActiveBookingsForRoom(int roomId, DateTime? onDate = null)
        {
            // Use provided date or current time
            var pivot = onDate ?? DateTime.Now;

            // Get bookings that overlap with the pivot date
            return _context.Bookings
                           .Where(b => b.RoomId == roomId // Filter by RoomId
                                    && b.CheckInDate <= pivot // Check if booking starts before or on the pivot date
                                    && b.CheckOutDate > pivot) // Check if booking ends after the pivot date
                           .Include(b => b.Guest) // Include guest details
                           .ToList(); // Execute query and return list
        }

        // Add a new booking
        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking); // Add entity to context
            _context.SaveChanges();         // Save changes to database
        }

        // Update an existing booking
        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking); 
            _context.SaveChanges();            
        }


    }
}
