namespace HaberPortali.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string NewsTitle { get; set; } = null!;

        public string Date { get; set; }

        public bool IsActive { get; set; }
    }
}
