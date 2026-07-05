using System.Collections.Generic;

namespace LostAndFound.Models
{
    public class ClaimsIndexViewModel
    {
        public List<Claim> MyClaims { get; set; } = new();
        public List<Claim> IncomingClaims { get; set; } = new();
    }
}
