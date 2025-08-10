using SimpleHotelRoomManagementProjectORM.Models;
using SimpleHotelRoomManagementProjectORM.Repository;
using SimpleHotelRoomManagementProjectORM.Services;

namespace SimpleHotelRoomManagementProjectORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ===================== INITIALIZATION (DbContext + Repositories + Services) =====================

            HotelDbContext dbContext = new HotelDbContext();// Create the database context

            RoomRepository roomRepo = new RoomRepository(dbContext); // Room repository
            GuestRepository guestRepo = new GuestRepository(dbContext);// Guest repository
            BookingRepository bookingRepo = new BookingRepository(dbContext);// Booking repository
            ReviewRepository reviewRepo = new ReviewRepository(dbContext); // Review repository   


            RoomService roomService = new RoomService(roomRepo);// Room service (business logic for rooms)
            GuestService guestService = new GuestService(guestRepo); // Guest service (business logic for guests)
            BookingService bookingService = new BookingService(bookingRepo, roomRepo, guestRepo); // Booking service (business logic for bookings)
            ReviewService reviewService = new ReviewService(reviewRepo, bookingRepo, guestRepo);// Review service (business logic for reviews)

            // ===================== SIMPLE INTERACTIVE MENU =====================


            bool exit = false;                                            
            while (!exit)                                                 
            {
                Console.WriteLine("\n=== Hotel Management Menu ===");     
                Console.WriteLine("1. Add Room"); // Add a new room to the system                        
                Console.WriteLine("2. Register Guest"); // Register a new guest in the system                  
                Console.WriteLine("3. Make Booking"); // Make a booking for a guest in a room                     
                Console.WriteLine("4. Add Review");// Add a review for a guest's stay in a room                        
                Console.WriteLine("5. List Rooms"); // List all rooms in the system                       
                Console.WriteLine("6. List Guests"); // List all guests in the system                       
                Console.WriteLine("7. Exit"); // Exit the application                              
                Console.Write("Choose an option: "); // Prompt user for a choice                     

                string choice = Console.ReadLine(); // Read user input for menu choice                      
                Console.WriteLine();                                      

                switch (choice)                                           
                {
                    case "1":  // ADD ROOM                                           
                        Console.Write("Enter Room Number: ");              
                        string rNum = Console.ReadLine();                

                        Console.Write("Enter Room Type: ");                 
                        string rType = Console.ReadLine();          

                        Console.Write("Enter Daily Rate: ");          
                        string rateInput = Console.ReadLine();              
                        double rate;                                       

                        if (!double.TryParse(rateInput, out rate))        
                        {
                            Console.WriteLine(" Invalid rate.");           
                            break;                                         
                        }

                        string roomError;                                 
                        bool roomOk = roomService.CreateRoom(rNum, rType, rate, out roomError); 

                        if (roomOk)                                      
                            Console.WriteLine(" Room added successfully!");
                        else
                            Console.WriteLine($" Error: {roomError}");
                        break;

                    case "2":                                              
                        Console.Write("Enter Guest Name: ");               
                        string gName = Console.ReadLine();                  

                        Console.Write("Enter Guest Email: ");              
                        string gEmail = Console.ReadLine();                 

                        string guestError;                                
                        bool guestOk = guestService.RegisterGuest(gName, gEmail, out guestError); 

                        if (guestOk)
                            Console.WriteLine(" Guest registered successfully!");
                        else
                            Console.WriteLine($" Error: {guestError}");
                        break;

                    case "3":  // MAKE BOOKING
                        Console.Write("Enter Room ID: ");
                        string roomIdInput = Console.ReadLine();
                        int roomId;
                        if (!int.TryParse(roomIdInput, out roomId))
                        {
                            Console.WriteLine(" Invalid Room ID.");
                            break;
                        }

                        Console.Write("Enter Guest ID: ");
                        string guestIdInput = Console.ReadLine();
                        int guestId;
                        if (!int.TryParse(guestIdInput, out guestId))
                        {
                            Console.WriteLine(" Invalid Guest ID.");
                            break;
                        }

                        Console.Write("Enter Check-in Date (yyyy-mm-dd): ");
                        string inInput = Console.ReadLine();
                        DateTime checkIn;
                        if (!DateTime.TryParse(inInput, out checkIn))
                        {
                            Console.WriteLine(" Invalid Check-in date.");
                            break;
                        }

                        Console.Write("Enter Check-out Date (yyyy-mm-dd): ");
                        string outInput = Console.ReadLine();
                        DateTime checkOut;
                        if (!DateTime.TryParse(outInput, out checkOut))
                        {
                            Console.WriteLine(" Invalid Check-out date.");
                            break;
                        }

                        if (checkIn >= checkOut)
                        {
                            Console.WriteLine(" Check-out must be after Check-in.");
                            break;
                        }

                        // Note the parameter order matches your method
                        bookingService.CreateBooking(guestId, roomId, checkIn, checkOut);
                        Console.WriteLine(" Booking created successfully!");
                        break;




                    case "4":                                            
                        Console.Write("Enter Guest ID: ");
                        string revGuestIdInput = Console.ReadLine();
                        int revGuestId;
                        if (!int.TryParse(revGuestIdInput, out revGuestId))
                        {
                            Console.WriteLine(" Invalid Guest ID.");
                            break;
                        }

                        Console.Write("Enter Rating (1-5): ");     
                        string ratingInput = Console.ReadLine();
                        int rating;
                        if (!int.TryParse(ratingInput, out rating))        
                        {
                            Console.WriteLine(" Invalid rating.");
                            break;
                        }

                        Console.Write("Enter Comment: ");                  
                        string comment = Console.ReadLine();

                        string reviewError;                            
                        bool reviewOk = reviewService.AddReview(revGuestId, rating, comment, out reviewError); 

                        if (reviewOk)
                            Console.WriteLine(" Review added successfully!");
                        else
                            Console.WriteLine($" Error: {reviewError}");
                        break;

                    case "5":                                            
                        System.Collections.Generic.List<Room> rooms = roomService.GetAllRooms(); 
                        Console.WriteLine("\n--- Rooms ---");
                        foreach (Room room in rooms)                        
                        {
                            Console.WriteLine($"ID: {room.RoomId}, Number: {room.RoomNumber}, Rate: {room.DailyRate}, Available: {room.IsAvailable}");
                        }
                        break;

                    case "6":                                             
                        System.Collections.Generic.List<Guest> guests = guestService.GetAllGuests(); 
                        Console.WriteLine("\n--- Guests ---");
                        foreach (Guest guest in guests)
                        {
                            Console.WriteLine($"ID: {guest.GuestId}, Name: {guest.Name}, Email: {guest.Email}");
                        }
                        break;

                    case "7":                                              
                        exit = true;
                        break;

                    default:                                              
                        Console.WriteLine(" Invalid choice.");
                        break;
                }
            }
        }
    }
}
