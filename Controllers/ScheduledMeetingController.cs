using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMeetingRoomApi.Data;
using SmartMeetingRoomApi.Dtos;
using SmartMeetingRoomApi.Models;

namespace SmartMeetingRoomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduledMeetingController : ControllerBase
    {
        private readonly SmartMeetingRoomApiDbContext _context;

        public ScheduledMeetingController(SmartMeetingRoomApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScheduledMeetingDto>>> GetScheduledMeetings()
        {
            var meetings = await _context.ScheduledMeetings
                .Select(m => new ScheduledMeetingDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    StartTime = m.StartTime,
                    EndTime = m.EndTime,
                    RoomId = m.RoomId,
                    UserId = m.UserId
                }).ToListAsync();

            return Ok(meetings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduledMeetingDto>> GetScheduledMeeting(int id)
        {
            var m = await _context.ScheduledMeetings.FindAsync(id);
            if (m == null) return NotFound();

            return Ok(new ScheduledMeetingDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                StartTime = m.StartTime,
                EndTime = m.EndTime,
                RoomId = m.RoomId,
                UserId = m.UserId
            });
        }

        [HttpPost]
        public async Task<ActionResult<ScheduledMeetingDto>> CreateScheduledMeeting([FromBody] CreateScheduledMeetingDto dto)
        {
            // Check if room exists
            var roomExists = await _context.Rooms.AnyAsync(r => r.Id == dto.RoomId);
            if (!roomExists)
            {
                return BadRequest($"Room with ID {dto.RoomId} does not exist.");
            }

            var meeting = new ScheduledMeeting
            {
                Title = dto.Title,
                Description = dto.Description,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                RoomId = dto.RoomId,
                UserId = dto.UserId
            };

            _context.ScheduledMeetings.Add(meeting);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetScheduledMeeting), new { id = meeting.Id }, new ScheduledMeetingDto
            {
                Id = meeting.Id,
                Title = meeting.Title,
                Description = meeting.Description,
                StartTime = meeting.StartTime,
                EndTime = meeting.EndTime,
                RoomId = meeting.RoomId,
                UserId = dto.UserId
            });
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScheduledMeeting(int id, UpdateScheduledMeetingDto dto)
        {
            var meeting = await _context.ScheduledMeetings.FindAsync(id);
            if (meeting == null) return NotFound();

            if (dto.Title != null) meeting.Title = dto.Title;
            if (dto.Description != null) meeting.Description = dto.Description;
            if (dto.StartTime.HasValue) meeting.StartTime = dto.StartTime.Value;
            if (dto.EndTime.HasValue) meeting.EndTime = dto.EndTime.Value;
            if (dto.RoomId.HasValue) meeting.RoomId = dto.RoomId.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheduledMeeting(int id)
        {
            var meeting = await _context.ScheduledMeetings.FindAsync(id);
            if (meeting == null) return NotFound();

            _context.ScheduledMeetings.Remove(meeting);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
