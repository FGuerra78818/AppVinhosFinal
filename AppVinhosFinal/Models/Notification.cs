namespace AppVinhosFinal.Models
{
    // Entities/Notification.cs
    public enum NotificationDirection
    {
        UserToAdmin,
        AdminToUser
    }

    public class Notification
    {
        public int Id { get; set; }
        public int? QuintaId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
        public NotificationDirection Direction { get; set; }
    }
}
