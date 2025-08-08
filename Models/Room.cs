using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProjectORM.Models
{
    public class Room
    {
        [Key] // Primary key for EF
        public int RoomId { get; set; } // Unique identifier for the room
    }
}
