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
    public class BookingService
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
            var guest = _guestRepository.GetById(guestId); // Retrieve guest by ID
            if (guest == null) // Check if guest exists
                throw new Exception("Guest not found."); // Fail if guest does not exist

            // Validate room exists
            var room = _roomRepository.GetById(roomId); // Retrieve room by ID
            if (room == null) // Check if room exists
                throw new Exception("Room not found."); // Fail if room does not exist

            // Validate booking dates
            if (checkIn >= checkOut) // Check if check-in date is not after check-out date
                throw new Exception("Check-out date must be after check-in date."); // Fail if dates are invalid


            // Check for overlapping bookings
            var existingBookings = _bookingRepository.GetAll() // Retrieve all existing bookings
                .Where(b => b.RoomId == roomId && 
                            ((checkIn >= b.CheckIn && checkIn < b.CheckOut) || // Check if new check-in overlaps with existing bookings
                             (checkOut > b.CheckIn && checkOut <= b.CheckOut) || // Check if new check-out overlaps with existing bookings
                             (checkIn <= b.CheckIn && checkOut >= b.CheckOut))) // Check if new booking completely overlaps with existing bookings
                .ToList();// Convert to list

            if (existingBookings.Any()) // If there are any overlapping bookings
                throw new Exception("Room is already booked for the selected dates."); // Fail if room is already booked

            // Create new booking object
            Booking newBooking = new Booking 
            {
                GuestId = guestId, // Set the guest ID
                RoomId = roomId, // Set the room ID
                CheckIn = checkIn, // Set the check-in date
                CheckOut = checkOut // Set the check-out date
            };

            // Save booking
            _bookingRepository.Add(newBooking);

        }

        // Get all bookings
        public List<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAll(); // Retrieve all bookings from the repository
        }

        // Get booking by ID
        public Booking GetBookingById(int bookingId)
        {
            return _bookingRepository.GetById(bookingId); // Retrieve booking by ID
        }
    }
}
