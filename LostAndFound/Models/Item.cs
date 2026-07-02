using LostAndFound.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostAndFound.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public City City { get; set; }
        public string? Location { get; set; }
        public ItemType Status { get; set; }
        public ReportStatus ReportStatus { get; set; }
        [HiddenInput]
        public string? ImageBase64 { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateOnly LostOrFoundDate { get; set; }
        public bool IsResolved { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;
        public ICollection<VerificationQuestion> VerificationQuestions { get; set; } = new List<VerificationQuestion>();
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
 
}
