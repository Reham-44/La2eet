using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.models
{
    internal class Claim
    {
        public int ClaimId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string VerificationAnswer { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }


    }
}
