using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProjectORM.Models
{
    public class Review
    {
        [Key] // Primary key for EF
        public int ReviewId { get; set; } // Unique identifier for the review

        [Required] // Cannot be null
        public int GuestId { get; set; } // Foreign key to the Guest table

        [Required] // Cannot be null
        [Range(1, 5)]   // Enforce rating between 1 and 5
        public int Rating { get; set; } // Rating given by the guest

        public string Comment { get; set; } // Optional comment from the guest
    }
}
