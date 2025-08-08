using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    //// Interface to define the contract for Room repository operations
    public interface IRoomRepository
    {
        void AddRoom(Room room); // Get all rooms
        void DeleteRoom(int id);
        List<Room> GetAllRooms();
        Room GetRoomById(int id);
        void UpdateRoom(Room room);
    }
}