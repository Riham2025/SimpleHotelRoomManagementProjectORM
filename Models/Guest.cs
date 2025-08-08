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

    }
}
