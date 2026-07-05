namespace LostAndFound.Models
{
   
        public class AdminDashboardViewModel
        {
            public int TotalReports { get; set; }
            public int LostReports { get; set; }
            public int FoundReports { get; set; }
            public int ActiveClaims { get; set; }

            public List<Item> RecentItems { get; set; } = new List<Item>();

            public Dictionary<string, int> CitySummary { get; set; } = new Dictionary<string, int>();
        }
    
}
