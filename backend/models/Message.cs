namespace backend.Models
{
    public class Message
    {
        public string? Text { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Id { get; set; }
    }

    public class MessageResponse
    {
        public string? Message { get; set; }
        public string? Id { get; set; }
        public DateTime ReceivedAt { get; set; }
        public int Length { get; set; }
    }
}