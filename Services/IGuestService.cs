using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public interface IGuestService
    {
        bool DeleteGuest(int id, out string error);
        List<Guest> GetAllGuests();
        Guest GetGuestByEmail(string email);
        Guest GetGuestById(int id);
        bool RegisterGuest(string name, string email, out string error);
        bool UpdateGuestEmail(int id, string newEmail, out string error);
        bool UpdateGuestName(int id, string newName, out string error);
    }
}