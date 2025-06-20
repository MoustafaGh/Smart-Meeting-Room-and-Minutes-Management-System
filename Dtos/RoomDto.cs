using System.ComponentModel.DataAnnotations;

namespace SmartMeetingRoomApi.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int Capacity { get; set; }
    }

    public class CreateRoomDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }
    }

    public class UpdateRoomDto
    {
        public string? Name { get; set; }
        public string? Location { get; set; }

        [Range(1, int.MaxValue)]
        public int? Capacity { get; set; }
    }
}