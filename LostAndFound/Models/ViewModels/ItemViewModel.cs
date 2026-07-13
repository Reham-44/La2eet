using LostAndFound.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostAndFound.Models.ViewModels
{
    public class ItemViewModel
    {

       [Required(ErrorMessage = "اسم الغرض مطلوب")]
            public string Title { get; set; }

            [Required(ErrorMessage = "الوصف مطلوب")]
            public string Description { get; set; }

            [Required(ErrorMessage = "المدينة مطلوبة")]
            public City City { get; set; }
        public string? Location { get; set; }

        [Required(ErrorMessage = "التاريخ مطلوب")]
        public DateOnly? LostOrFoundDate { get; set; } 

        public ItemType Status { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public List<VerificationQuestionViewModel> Questions { get; set; } = new();
        }
   
}
