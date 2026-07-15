namespace LostAndFound.Models.ViewModels
{
    public class ItemDetailsViewModel
    {
        public Item Item { get; set; } = null!;
        public Claim Claim { get; set; }
        public List<Item> SimilarItems { get; set; } = new();
    }
}
