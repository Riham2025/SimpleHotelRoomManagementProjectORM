using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    //// Interface to define the contract for Room repository operations
    public interface IRoomRepository
    {
        void AddRoom(Room room); // Get all rooms
        void DeleteRoom(int id);  // Get a specific room by ID
        List<Room> GetAllRooms(); // Add a new room
        Room GetRoomById(int id);
        void UpdateRoom(Room room);
    }
}