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

        }
    }
}
