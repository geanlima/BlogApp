using AutoMapper;
using BlogApp.Application.DTOs.Requests;
using BlogApp.Application.DTOs.Responses;
using BlogApp.Application.Services.Interfaces;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;
using System.Text;

namespace BlogApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            var user = new User(request.Username, request.Email, HashPassword(request.Password));
            await _userRepository.AddAsync(user);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task UpdateUserAsync(int id, UserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.SetUsername(request.Username);
                user.SetEmail(request.Email);
                user.SetPasswordHash(HashPassword(request.Password));
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
}
