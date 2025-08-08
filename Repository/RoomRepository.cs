using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM.Repository
{
    // Implementation of IRoomRepository using Entity Framework Core
    public class RoomRepository
    {

        private readonly HotelDbContext _context; // Database context for accessing the database

        // Constructor: inject the database context
        public RoomRepository(HotelDbContext context) 
        {
            _context = context; // Assign the injected context to the private field
        }

        // Get all rooms from the database
        public List<Room> GetAllRooms()
        {
            return _context.Rooms.ToList(); // Retrieve all rooms from the Rooms table
        }

        // Get a specific room by ID
        public Room GetRoomById(int id)
        {
            return _context.Rooms.FirstOrDefault(r => r.RoomId == id); // Retrieve the room with the specified ID from the Rooms table
        }

        // Add a new room to the database
        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room); 
            _context.SaveChanges(); // Save the change to the database
        }

    }
}
