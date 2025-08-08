using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleHotelRoomManagementProjectORM.Models;

namespace SimpleHotelRoomManagementProjectORM
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; } // Represents the Rooms table in the database
        public DbSet<Guest> Guests { get; set; } // Represents the Guests table in the database
        public DbSet<Booking> Bookings { get; set; } // Represents the Bookings table in the database
        public DbSet<Review> Reviews { get; set; } // Represents the Reviews table in the database

    }
}
