using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    // High-level contract for Room operations (consumed by UI/controllers)
    public interface IRoomService
    {
        bool ChangeRoomNumber(int roomId, string newRoomNumber, out string error); // Change room number with uniqueness and non-empty checks
        bool CreateRoom(string roomNumber, string type, double dailyRate, out string error); // Create a new room after validating number uniqueness and minimum rate
        bool DeleteRoom(int roomId, out string error); // Delete a room by ID (may add business rules later, e.g., cannot delete if reserved)
        List<Room> FindAvailableRooms(); // Find all available rooms
        List<Room> FindAvailableRoomsByType(string type); // Find all available rooms of a specific type
        List<Room> GetAllRooms(); // Return all rooms
        Room GetRoomById(int roomId); // Return a single room by ID (or null if not found)
        bool SetAvailability(int roomId, bool isAvailable, out string error); // Set room availability status (e.g., mark as reserved or available)
        bool UpdateDailyRate(int roomId, double newRate, out string error); // Update the daily rate (business rule: minimum 100, cannot be negative)
    }
}