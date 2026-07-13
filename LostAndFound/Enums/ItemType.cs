using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Enums
{
    public enum ItemType
        {
        [Display(Name = "مفقود")]
        Lost,
        [Display(Name = "وُجد")]

        Found,
        [Display(Name = "رجع لصاحبه")]

        Returned
    }   
}
