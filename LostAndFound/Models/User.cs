using LostAndFound.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public Role Role { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }
}