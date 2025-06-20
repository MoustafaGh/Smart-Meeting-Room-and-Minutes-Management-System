using System.Text.Json.Serialization;

namespace SmartMeetingRoomApi.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Default to empty string if not provided  
        public string? Location { get; set; }  // Default to empty string if not provided
        public int Capacity { get; set; }


        [JsonIgnore]
        public ICollection<ScheduledMeeting>? ScheduledMeetings { get; set; } // Navigation property to ScheduledMeeting
    }
}
