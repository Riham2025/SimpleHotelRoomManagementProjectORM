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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Connection string to local SQL Server
            optionsBuilder.UseSqlServer(@"Server=.;Database=HotelDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure Room entity
            // RoomNumber is required and limited to 10 characters, and made unique
            modelBuilder.Entity<Room>()
                        .Property(r => r.RoomNumber)
                        .IsRequired() 
                        .HasMaxLength(10); 
            modelBuilder.Entity<Room>()
                        .HasIndex(r => r.RoomNumber)
                        .IsUnique(); // RoomNumber must be unique


            // Configure Guest entity: Name is required with a max length of 50
            modelBuilder.Entity<Guest>() 
                        .Property(g => g.Name)
                        .IsRequired()
                        .HasMaxLength(50);

            // Configure Review entity: Each review belongs to one guest
            modelBuilder.Entity<Review>()
                        .HasOne(r => r.Guest) //A review has one guest
                        .WithMany(g => g.Reviews) //A guest can have many reviews
                        .HasForeignKey(r => r.GuestId); 


        }
    }
}