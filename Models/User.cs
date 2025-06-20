using System.Text.Json.Serialization;

namespace SmartMeetingRoomApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; } // Store hashed password for security
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Default to current time
        public bool IsActive { get; set; } = true; // Default value is true, indicating the user is active
        public string? Role { get; set; }


        [JsonIgnore]
        public ICollection<ScheduledMeeting>? ScheduledMeetings { get; set; }

        [JsonIgnore]
        public ICollection<Notification>? Notifications { get; set; }

        [JsonIgnore]
        public ICollection<MoM>? MoMs { get; set; }

        [JsonIgnore]
        public ICollection<MeetingAttendee>? MeetingAttendees { get; set; } // Navigation property to MeetingAttendee
    }
}
