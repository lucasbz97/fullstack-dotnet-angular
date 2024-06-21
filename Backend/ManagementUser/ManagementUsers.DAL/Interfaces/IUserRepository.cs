using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementUsers.DAL.DTOs.Request;
using ManagementUsers.DAL.DTOs.Response;
using ManagementUsers.DAL.Entities;


namespace ManagementUsers.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<UserResponseDTO?> GetUserByIdAsync(long id);
        Task<UserResponseDTO?> AddUserAsync(UserRequestDTO user);
        Task<IEnumerable<UserResponseDTO?>> GetAllUsersAsync();
        Task<UserResponseDTO> UpdateUserAsync(UserRequestDTO user);
        Task<UserResponseDTO?> DeleteUserAsync(long id);
    }
}
