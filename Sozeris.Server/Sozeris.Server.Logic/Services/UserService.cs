using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Sozeris.Server.Data.Repositories.Interfaces;
using Sozeris.Server.Logic.Services.Interfaces;
using Sozeris.Server.Models.DTO;
using Sozeris.Server.Models.Entities;

namespace Sozeris.Server.Logic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        
        return user;
    }

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        var user = await _userRepository.GetUserByLoginAsync(login);
        
        return user;
    }
    
    public async Task<bool> CreateUserAsync(UserDTO userDto)
    {
        userDto.Password = HashPassword(userDto.Password);
        
        var user = _mapper.Map<User>(userDto);
        
        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<bool> UpdateUserAsync(UserDTO user)
    {
        var oldUser = await _userRepository.GetUserByIdAsync(user.Id);
        
        if (oldUser == null) return false;
        
        _mapper.Map(user, oldUser);
        
        oldUser.Password = HashPassword(user.Password);
        
        return await _userRepository.UpdateUserAsync(oldUser);
    }

    public async Task<bool> DeleteUserByIdAsync(int userId)
    {
        return await _userRepository.DeleteUserByIdAsync(userId);
    }
    
    public string HashPassword(string password)
    {
        // добавляем щепотку соли
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }
    public bool VerifyPassword(string password, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
            {
                return false;
            }
        }
        return true;
    }
}