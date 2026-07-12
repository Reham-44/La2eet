using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models.ViewModels
{
    public class VerificationQuestionViewModel
    {
        [Required(ErrorMessage = "السؤال مطلوب")]
        public string? QuestionText { get; set; }

        [Required(ErrorMessage = "الإجابة مطلوبة")]
        public string? ExpectedAnswerHash { get; set; }
    }
}
