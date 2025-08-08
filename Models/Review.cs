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
    }
}
