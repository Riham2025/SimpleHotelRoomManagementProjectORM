using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    //// Interface to define the contract for Room repository operations
    public interface IRoomRepository
    {
        void AddRoom(Room room); // Add a new room to the database
        void DeleteRoom(int id); // Delete a room by its ID 
        List<Room> GetAllRooms(); // Get all rooms from the database
        Room GetRoomById(int id); // Get a specific room by its ID
        void UpdateRoom(Room room); // Update an existing room in the database
    }
}