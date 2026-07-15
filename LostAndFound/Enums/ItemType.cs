using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Enums
{
    public enum ItemType
        {
        [Display(Name = "مفقود")]
        Lost,

        [Display(Name = "موجود")]
        Found,

        [Display(Name = "رجعت لصاحبها")]
        Returned
    }   
}
