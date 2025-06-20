namespace SmartMeetingRoomApi.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int ScheduledMeetingId { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }

    public class CreateNotificationDto
    {
        public int ScheduledMeetingId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class UpdateNotificationDto
    {
        public bool IsRead { get; set; }
    }
}
