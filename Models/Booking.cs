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

        [Range(1, 30)] // Enforce nights between 1 and 30
        public int Nights { get; set; } // Number of nights booked

        public DateTime BookingDate { get; set; } = DateTime.Now; // Date of booking, defaults to now

        // Navigation properties
        public Room Room { get; set; } // Associated room for the booking
        public Guest Guest { get; set; } // Associated guest for the booking

        // Additional properties can be added as needed

    }
}
