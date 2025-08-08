using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProjectORM.Models
{
    public class Booking
    {
        [Key] // Primary key for EF
        public int BookingId { get; set; } // Unique identifier for the booking

        [Required] // Cannot be null
        public int RoomId { get; set; } // Foreign key to the Room table

        [Required] // Cannot be null
        public int GuestId { get; set; } // Foreign key to the Guest table
    }
}
