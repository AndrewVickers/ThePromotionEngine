namespace ThePromotionEngine.Library.Models
{
    public class Promotion
    {
        public int Priority { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PrimaryItemId { get; set; }

        public Promotion GetHighestPriorityPromotionForItem(string item)
        {
            return null;
        }
    }
}
