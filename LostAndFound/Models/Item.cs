using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.models
{
    internal class Item
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
