using System.Text.Json.Serialization;

namespace SmartMeetingRoomApi.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int ScheduledMeetingId { get; set; } // Foreign key to ScheduledMeeting
        public string? UserId { get; set; } // User who receives the notification , FK from User table
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Default to current time
        public bool IsRead { get; set; } = false; // Default value is false

        [JsonIgnore]
        public User? Users { get; set; } // Navigation property to User

        [JsonIgnore]
        public ScheduledMeeting? ScheduledMeeting { get; set; } // Navigation property to ScheduledMeeting

    }
}
