using SmartMeetingRoomApi.Dtos;
using SmartMeetingRoomApi.Models;
using System;

namespace SmartMeetingRoomApi.services
{
    public interface IAuthService
    {
        Task<User?> CreateUser(CreateUserDto request);
        Task<String?> Login(UserLoginDto request);

    }
}