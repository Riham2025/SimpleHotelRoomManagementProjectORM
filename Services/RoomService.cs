using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public class RoomService
    {
        // Reference to the data-access layer (Room repository)
        private readonly IRoomRepository _roomRepo;

        // Inject the repository via constructor (DI-friendly)
        public RoomService(IRoomRepository roomRepo)
        { }
    }
}
