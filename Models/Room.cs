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

        [Required] // Cannot be null
        public string RoomNumber { get; set; }// Room number (e.g., "101", "102")

        [Range(100, double.MaxValue)] // Enforce rate >= 100
        public double DailyRate { get; set; } // Daily rate for the room

        [Required] // Cannot be null
        public bool IsReserved { get; set; } // Indicates if the room is reserved

        // Navigation property to related bookings
        public ICollection<Booking> Bookings { get; set; }

    }
}
