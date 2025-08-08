using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    // Interface that defines all operations allowed on the Guest entity
    public interface IGuestRepository
    {
        void AddGuest(Guest guest); // Add a new guest
        void DeleteGuest(int id); // Delete a guest by ID
        List<Guest> GetAllGuests(); // Get all guests
        Guest GetGuestByEmail(string email); // Get a guest by email
        Guest GetGuestById(int id);
        void UpdateGuest(Guest guest);
    }
}