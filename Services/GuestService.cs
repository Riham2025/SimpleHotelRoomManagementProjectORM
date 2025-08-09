using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProjectORM.Models;
using SimpleHotelRoomManagementProjectORM.Repository;

namespace SimpleHotelRoomManagementProjectORM.Services
{
    public class GuestService
    {
        // Reference to the data-access layer for guests
        private readonly IGuestRepository _guestRepo;

        // Inject the repository through the constructor (DI-friendly)
        public GuestService(IGuestRepository guestRepo)
        {
            _guestRepo = guestRepo; // Store the repository for later use
        }

        // Get all guests (no extra rules needed here)
        public List<Guest> GetAllGuests()
        {
            return _guestRepo.GetAllGuests(); // Delegate to repository
        }

        // Get a guest by ID (pass-through)
        public Guest GetGuestById(int id)
        {
            return _guestRepo.GetGuestById(id); // Delegate to repository
        }

        // Get guest by email (pass-through)
        public Guest GetGuestByEmail(string email)
        {
            return _guestRepo.GetGuestByEmail(email); // Delegate to repository
        }

        // Register a new guest with validations (non-empty name, valid and unique email)
        public bool RegisterGuest(string name, string email, out string error)
        {
            error = string.Empty; // Initialize error output

            // Basic name validation: must not be null/whitespace
            if (string.IsNullOrWhiteSpace(name))
            {
                error = "Name cannot be empty."; 
                return false; // Fail early
            }

            // Basic email validation using a simple regex pattern
            if (!IsValidEmail(email))
            {
                error = "Email is not valid."; // Fail early
                return false;
            }

            // Ensure the email is unique across all guests
            if (_guestRepo.GetGuestByEmail(email) != null) //Check if email already exists
            {
                error = "A guest with the same email already exists."; 
                return false; 
            }

            // Create the entity
            var guest = new Guest
            {
                Name = name.Trim(),   // Normalize name
                Email = email.Trim()  // Normalize email
            };

            // Persist through repository
            _guestRepo.AddGuest(guest);

            return true; // Registration successful

        }

        // Update only the name after validation
        public bool UpdateGuestName(int id, string newName, out string error)
        {
            error = string.Empty; // Reset error

            // Validate new name
            if (string.IsNullOrWhiteSpace(newName)) //Check if new name is empty
            {
                error = "New name cannot be empty."; // Fail early
                return false;
            }

            // Load the existing entity
            var existing = _guestRepo.GetGuestById(id); //Check if guest exists
            if (existing == null) //Check if guest exists
            {
                error = "Guest not found."; // Fail early
                return false;
            }

            // Apply change
            existing.Name = newName.Trim();

            // Save changes
            _guestRepo.UpdateGuest(existing);

            return true; // Success
        }

        // Update only the email after validation and uniqueness check
        public bool UpdateGuestEmail(int id, string newEmail, out string error)
        {
            error = string.Empty; // Reset error

            // Validate email format
            if (!IsValidEmail(newEmail)) //Check if new email is valid
            {
                error = "New email is not valid."; // Fail early
                return false;
            }

            // Load the existing entity
            var existing = _guestRepo.GetGuestById(id);//Check if guest exists
            if (existing == null)
            {
                error = "Guest not found."; // Fail early
                return false;
            }

            // If email is unchanged, consider this a no-op success
            if (string.Equals(existing.Email?.Trim(), newEmail?.Trim(), System.StringComparison.OrdinalIgnoreCase)) 
            {
                return true; // Nothing to update
            }

            // Ensure uniqueness for the new email
            var conflict = _guestRepo.GetGuestByEmail(newEmail); //Check if email already exists
            if (conflict != null && conflict.GuestId != id) //Check if email belongs to another guest
            {
                error = "Another guest with this email already exists."; // Fail early
                return false;
            }

            // Apply change
            existing.Email = newEmail.Trim();

        }

    }
}
