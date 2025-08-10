using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Models;
using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    // Implements IBookingService to handle booking operations
    public class BookingService : IBookingService
    {

        private readonly IBookingRepository _bookingRepository;  // To interact with booking data storage
        private readonly IRoomRepository _roomRepository; // To interact with room data
        private readonly IGuestRepository _guestRepository; // To interact with guest data

        // Constructor for dependency injection
        public BookingService(IBookingRepository bookingRepo, IRoomRepository roomRepo, IGuestRepository guestRepo)
        {
            _bookingRepository = bookingRepo; // Store the booking repository
            _roomRepository = roomRepo; // Store the room repository
            _guestRepository = guestRepo; // Store the guest repository
        }

        // Create a new booking
        public void CreateBooking(int guestId, int roomId, DateTime checkIn, DateTime checkOut)
        {
            // Validate guest exists
            var guest = _guestRepository.GetGuestById(guestId); // Retrieve guest by ID
            if (guest == null) // Check if guest exists
                throw new Exception("Guest not found."); // Fail if guest does not exist

            // Validate room exists
            var room = _roomRepository.GetRoomById(roomId); // Retrieve room by ID
            if (room == null) // Check if room exists
                throw new Exception("Room not found."); // Fail if room does not exist

            // Validate booking dates
            if (checkIn >= checkOut) // Check if check-in date is not after check-out date
                throw new Exception("Check-out date must be after check-in date."); // Fail if dates are invalid


            // Check for overlapping bookings
            var overlaps = _bookingRepository.GetAllBookings()
     .Where(b => b.RoomId == roomId &&
                 checkIn < b.CheckOutDate && b.CheckInDate < checkOut)
     .Any();
            if (overlaps) // If there are overlapping bookings
                throw new Exception("Room is already booked for the selected dates."); // Fail if room is already booked



            // Create new booking object using the correct property names
            var newBooking = new Booking
            {
                GuestId = guestId, // Associate booking with guest
                RoomId = roomId, // Associate booking with room
                CheckInDate = checkIn, // Set check-in date
                CheckOutDate = checkOut // Set check-out date
            };

            // Save booking
            _bookingRepository.AddBooking(newBooking);

        }

        // Get all bookings
        public List<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();   // Retrieve all bookings from the repository
        }

        // Get booking by ID
        public Booking GetBookingById(int bookingId)
        {
            return _bookingRepository.GetBookingById(bookingId); // Retrieve booking by ID
        }

        // Cancel booking
        public void CancelBooking(int bookingId)
        {
            var booking = _bookingRepository.GetBookingById(bookingId); // Retrieve booking by ID
            if (booking == null) // Check if booking exists
                throw new Exception("Booking not found."); // Fail if booking does not exist

            _bookingRepository.DeleteBooking(bookingId); // Delete the booking by ID
        }

        // Update booking dates
        public void UpdateBookingDates(int bookingId, DateTime newCheckIn, DateTime newCheckOut) // Update booking dates
        {
            var booking = _bookingRepository.GetBookingById(bookingId); // Retrieve booking by ID
            if (booking == null) // Check if booking exists
                throw new Exception("Booking not found."); // Fail if booking does not exist

            if (newCheckIn >= newCheckOut)
                throw new Exception("Check-out date must be after check-in date."); // Fail if dates are invalid

            // Check for overlapping bookings excluding current booking
            var conflict = _bookingRepository.GetAllBookings()
    .Where(b => b.RoomId == booking.RoomId &&
                b.BookingId != bookingId &&
                newCheckIn < b.CheckOutDate && b.CheckInDate < newCheckOut)
    .Any();

            if (conflict)
                throw new Exception("Room is already booked for the new dates.");

            // Update booking details
            booking.CheckInDate = newCheckIn;
            booking.CheckOutDate = newCheckOut;
            

            _bookingRepository.UpdateBooking(booking); // Save updated booking

        }
    }
}