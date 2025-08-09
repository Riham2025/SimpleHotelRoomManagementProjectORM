using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Models;
using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public class RoomService
    {
        // Reference to the data-access layer (Room repository)
        private readonly IRoomRepository _roomRepo;

        // Inject the repository via constructor (DI-friendly)
        public RoomService(IRoomRepository roomRepo)
        {
            _roomRepo = roomRepo; // Store for later use in methods
        }

        // Create a new room after validating number uniqueness and minimum rate
        public bool CreateRoom(string roomNumber, string type, double dailyRate, out string error)
        {
            error = string.Empty; // Initialize error output

            // Validate room number (non-empty)
            if (string.IsNullOrWhiteSpace(roomNumber)) //Check if room number is empty
            {
                error = "Room number cannot be empty.";
                return false; // Fail fast
            }

            // Validate rate (per requirements: daily rate must be >= 100)
            if (dailyRate < 100) //Check if daily rate is less than 100
            {
                error = "Daily rate must be at least 100.";
                return false; // Fail fast
            }

            // Enforce uniqueness at the service level (in addition to the DB unique index)
            var allRooms = _roomRepo.GetAllRooms(); // Pull current rooms
            if (allRooms.Any(r => string.Equals(r.RoomNumber?.Trim(), roomNumber.Trim(), StringComparison.OrdinalIgnoreCase))) // Check if room number already exists
            {
                error = "A room with the same number already exists."; // Fail fast
                return false;
            }

            // Create the entity to persist
            var room = new Room
            {
                RoomNumber = roomNumber.Trim(), // Normalize input
                Type = type?.Trim(), // Normalize type (optional)          
                DailyRate = dailyRate, // Set the daily rate         
                IsAvailable = true   // Set initial availability to true           
            };

            // Persist through the repository
            _roomRepo.AddRoom(room);

            // Success
            return true;

        }

        // Change room number with uniqueness and non-empty checks
        public bool ChangeRoomNumber(int roomId, string newRoomNumber, out string error)
        {
            error = string.Empty; // Reset error

            // Validate new number
            if (string.IsNullOrWhiteSpace(newRoomNumber)) //Check if new room number is empty
            {
                error = "New room number cannot be empty.";
                return false; // Fail fast
            }

            // Load existing room
            var existing = _roomRepo.GetRoomById(roomId); //Check if room exists
            if (existing == null)
            {
                error = "Room not found."; // Fail fast
                return false;
            }

            // If unchanged (ignoring case/whitespace), we’re done
            if (string.Equals(existing.RoomNumber?.Trim(), newRoomNumber.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return true; // No change needed
            }

            // Check uniqueness against other rooms
            var allRooms = _roomRepo.GetAllRooms(); // Get all existing rooms
            if (allRooms.Any(r => r.RoomId != roomId && // Check if room ID is different
                                  string.Equals(r.RoomNumber?.Trim(), newRoomNumber.Trim(), StringComparison.OrdinalIgnoreCase))) // Check if new room number already exists
            {

                error = "Another room with this number already exists."; // Fail fast
                return false; // Early exit
            }

            // Apply change and persist
            existing.RoomNumber = newRoomNumber.Trim(); // Normalize input
            _roomRepo.UpdateRoom(existing); // Save changes

            // Success
            return true;

        }

        // Update the daily rate (business rule: minimum 100)
        public bool UpdateDailyRate(int roomId, double newRate, out string error) 
        {
            error = string.Empty; // Reset error

            // Validate rate
            if (newRate < 100) // Check if new rate is less than 100
            {
                error = "Daily rate must be at least 100."; // Fail fast
                return false;// Early exit
            }

            // Load existing room
            var existing = _roomRepo.GetRoomById(roomId); //Check if room exists
            if (existing == null) 
            {
                error = "Room not found."; // Fail fast
                return false; // Early exit
            }

            // Apply and persist
            existing.DailyRate = newRate; // Normalize input
            _roomRepo.UpdateRoom(existing); // Save changes

            // Success
            return true;

        }

        // Set availability flag (true = available, false = unavailable)
        public bool SetAvailability(int roomId, bool isAvailable, out string error)
        {
            error = string.Empty; // Reset error

            // Load existing room
            var existing = _roomRepo.GetRoomById(roomId); //Check if room exists

            if (existing == null) // Check if room exists
            {
                error = "Room not found."; // Fail fast
                return false; // Early exit
            }

            // Apply and persist
            existing.IsAvailable = isAvailable; // Normalize input
            _roomRepo.UpdateRoom(existing); // Save changes

            // Success
            return true;

        }

        // Delete a room by ID (you can later add rules to prevent deletion if active bookings exist)
        public bool DeleteRoom(int roomId, out string error)
        {
            error = string.Empty; // Reset error

            // Ensure the room exists
            var existing = _roomRepo.GetRoomById(roomId);
            if (existing == null)
            {
                error = "Room not found.";
                return false;
            }

            // Business rule example (future):
            // if (_bookingRepo.GetActiveBookingsForRoom(roomId).Any()) { error = "..."; return false; }

            // Proceed with deletion
            _roomRepo.DeleteRoom(roomId);

            // Success
            return true;

        }

        // Return all rooms (pass-through)
        public List<Room> GetAllRooms()
        {
            return _roomRepo.GetAllRooms(); // Delegate to repository
        }

        // Return a single room by its ID (pass-through)
        public Room GetRoomById(int roomId)
        {
            return _roomRepo.GetRoomById(roomId);
        }

        // Helper: list available rooms only
        public List<Room> FindAvailableRooms()
        {
            return _roomRepo.GetAllRooms()
                            .Where(r => r.IsAvailable) // Filter in memory; alternatively create a repo method if you prefer server-side
                            .ToList(); // Return only available rooms
        }

        // Helper: list available rooms filtered by type (case-insensitive)
        public List<Room> FindAvailableRoomsByType(string type)
        {
            var wanted = type?.Trim(); // Normalize type
            return _roomRepo.GetAllRooms()
                            .Where(r => r.IsAvailable
                                     && !string.IsNullOrWhiteSpace(r.Type)
                                     && string.Equals(r.Type.Trim(), wanted, StringComparison.OrdinalIgnoreCase))
                            .ToList();
        }
    }
}
