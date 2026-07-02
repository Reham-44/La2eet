using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Enums
{
    public enum City
    {
        [Display(Name = "القاهرة")]
        Cairo,

        [Display(Name = "الجيزة")]
        Giza,

        [Display(Name = "الإسكندرية")]
        Alexandria,

        [Display(Name = "الدقهلية")]
        Dakahlia,

        [Display(Name = "الشرقية")]
        Sharqia,

        [Display(Name = "الغربية")]
        Gharbia,

        [Display(Name = "المنوفية")]
        Minoufia,

        [Display(Name = "القليوبية")]
        Qalyubia,

        [Display(Name = "البحيرة")]
        Beheira,

        [Display(Name = "كفر الشيخ")]
        KafrElSheikh,

        [Display(Name = "دمياط")]
        Damietta,

        [Display(Name = "بورسعيد")]
        PortSaid,

        [Display(Name = "الإسماعيلية")]
        Ismailia,

        [Display(Name = "السويس")]
        Suez,

        [Display(Name = "الفيوم")]
        Fayoum,

        [Display(Name = "بني سويف")]
        BeniSuef,

        [Display(Name = "المنيا")]
        Minya,

        [Display(Name = "أسيوط")]
        Assiut,

        [Display(Name = "سوهاج")]
        Sohag,

        [Display(Name = "قنا")]
        Qena,

        [Display(Name = "الأقصر")]
        Luxor,

        [Display(Name = "أسوان")]
        Aswan,

        [Display(Name = "البحر الأحمر")]
        RedSea,

        [Display(Name = "الوادي الجديد")]
        NewValley,

        [Display(Name = "مطروح")]
        Matrouh,

        [Display(Name = "شمال سيناء")]
        NorthSinai,

        [Display(Name = "جنوب سيناء")]
        SouthSinai
    }
}
