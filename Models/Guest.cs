using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProjectORM.Models
{
    public class Guest
    {
        [Key] // Primary key for EF
        public int GuestId { get; set; } // Unique identifier for the guest

        [Required] // Cannot be null
        [MaxLength(50)] // Maximum length for the name
        public string Name { get; set; } // Guest's name

        [EmailAddress] // Validates that the string is a valid email format
        public string Email { get; set; } // Guest's email address

        public ICollection<Booking> Bookings { get; set; } // List of bookings associated with this guest

        public ICollection<Review> Reviews { get; set; } // List of reviews written by this guest

    }
}
