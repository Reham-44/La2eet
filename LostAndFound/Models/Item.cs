using System;

namespace LostAndFound.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LostOrFoundDate { get; set; }
        public bool IsResolved { get; set; }

        public int UserID { get; set; }
    }
}
