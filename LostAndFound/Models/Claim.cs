using System;

namespace LostAndFound.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string VerificationAnswer { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
    }
}
