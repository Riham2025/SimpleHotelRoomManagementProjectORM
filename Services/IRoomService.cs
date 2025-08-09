using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public interface IRoomService
    {
        bool ChangeRoomNumber(int roomId, string newRoomNumber, out string error); // Change room number with uniqueness and non-empty checks
        bool CreateRoom(string roomNumber, string type, double dailyRate, out string error);
        bool DeleteRoom(int roomId, out string error);
        List<Room> FindAvailableRooms();
        List<Room> FindAvailableRoomsByType(string type);
        List<Room> GetAllRooms();
        Room GetRoomById(int roomId);
        bool SetAvailability(int roomId, bool isAvailable, out string error);
        bool UpdateDailyRate(int roomId, double newRate, out string error);
    }
}