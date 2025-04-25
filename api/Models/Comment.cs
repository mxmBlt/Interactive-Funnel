namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? StockId { get; set; }
        // navigation key allowing the comment to be linked to a stock

        public Stock? Stock { get; set; }


    }
}