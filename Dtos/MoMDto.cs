namespace SmartMeetingRoomApi.Dtos
{
    public class MoMDto
    {
        public int Id { get; set; }
        public int ScheduledMeetingId { get; set; }
        public string? CreatedBy { get; set; }
        public string? Summary { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateMoMDto
    {
        public int ScheduledMeetingId { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public class UpdateMoMDto
    {
        public string? Summary { get; set; }
        public string? Notes { get; set; }
    }
}
