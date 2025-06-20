using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMeetingRoomApi.Data;
using SmartMeetingRoomApi.Dtos;
using SmartMeetingRoomApi.Models;

namespace SmartMeetingRoomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly SmartMeetingRoomApiDbContext _context;

        public RoomController(SmartMeetingRoomApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoomDto>>> GetRoom()
        {
            var rooms = await _context.Rooms
                .Select(r => new RoomDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Location = r.Location,
                    Capacity = r.Capacity
                }).ToListAsync();

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();

            return Ok(new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Location = room.Location,
                Capacity = room.Capacity
            });
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom(CreateRoomDto dto)
        {
            var room = new Room
            {
                Name = dto.Name,
                Location = dto.Location,
                Capacity = dto.Capacity
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Location = room.Location,
                Capacity = room.Capacity
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, UpdateRoomDto dto)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();

            if (dto.Name != null) room.Name = dto.Name;
            if (dto.Location != null) room.Location = dto.Location;
            if (dto.Capacity.HasValue) room.Capacity = dto.Capacity.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
