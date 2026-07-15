using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models.ViewModels
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        [Display(Name = "الاسم الكامل")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "رقم الموبايل")]
        [Phone(ErrorMessage = "رقم الموبايل غير صحيح")]
        public string Phone { get; set; } = string.Empty;
    }
}