using System;

namespace LostAndFound.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        // Store hashed password as string (e.g. base64 or hex)
        public string PasswordHash { get; set; }
        // Phone as string to preserve leading zeros and special chars
        public string Phone { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; }
    }
}
