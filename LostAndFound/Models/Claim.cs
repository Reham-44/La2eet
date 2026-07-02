using LostAndFound.Enums;
using System;
using System.Collections.Generic;

namespace LostAndFound.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public ClaimStatus ClaimStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string VerificationAnswer { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

        public User User { get; set; } = null!;
        public Item Item { get; set; } = null!;
    }
}
