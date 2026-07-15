using System;
using System.Collections.Generic;

namespace LostAndFound.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

        public List<Claim> Claims { get; set; } = new List<Claim>();
        public int UserId { get; set; }
        public bool IsBanned { get; set; }
        public int TotalReports => Items.Count;
        public int ResolvedReports => Items.FindAll(i => i.IsResolved).Count;
        public int TotalClaims => Claims.Count;
    }
}