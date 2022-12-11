namespace APIExamen2.Models.DTOs
{
    public class AskDTO
    {
        public long AskId { get; set; }
        public DateTime Date { get; set; }
        public string AskDescription { get; set; } = null!;
        public int UserId { get; set; }
        public int AskStatusId { get; set; }
        public bool? IsStrike { get; set; }
        public string? ImageUrl { get; set; }
        public string? AskDetail { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
    }
}
