namespace LostAndFound.Models.ViewModels
{
    public class ItemDetailsViewModel
    {
        public Item Item { get; set; } = null!;
        public List<Item> SimilarItems { get; set; } = new();
    }
}
