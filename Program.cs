using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ===================== INITIALIZATION (DbContext + Repositories + Services) =====================

            HotelDbContext dbContext = new HotelDbContext();// Create the database context

            RoomRepository roomRepo = new RoomRepository(dbContext); // Room repository


        }
    }
}
