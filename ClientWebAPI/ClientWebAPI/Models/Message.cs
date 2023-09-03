namespace ClientWebAPI.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime SentTime { get; set; }
    }
}
