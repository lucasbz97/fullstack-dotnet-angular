
using ManagementUsers.DAL.Contexts;
using ManagementUsers.DAL.DTOs.Request;
using ManagementUsers.DAL.DTOs.Response;
using ManagementUsers.DAL.Entities;
using ManagementUsers.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementUsers.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(long id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Dependents)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return null;

            return new UserResponseDTO(user.Id, user.Name, user.Age);
        }

        public async Task<UserResponseDTO?> AddUserAsync(UserRequestDTO user)
        {
            var userEntity = new UserModel(user.Id, user.Name, user.Age);

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return new UserResponseDTO(userEntity.Id, userEntity.Name, userEntity.Age);
        }

        public async Task<IEnumerable<UserResponseDTO?>> GetAllUsersAsync()
        {
            var users = await _context.Users.Include(u => u.Dependents).ToListAsync();

            if (users.Count == 0)
                return null;

            var response = users.Select(dep => new UserResponseDTO(dep.Id, dep.Name, dep.Age));

            return response;
        }

        public async Task<UserResponseDTO> UpdateUserAsync(UserRequestDTO user)
        {
            var userEntity = await _context.Users.FindAsync(user.Id);

            if (userEntity is null)
                return null;

            userEntity.Age = user.Age;
            userEntity.Name = user.Name;
            userEntity.LastUpdatedAt = DateTime.UtcNow;

            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();

            return new UserResponseDTO(userEntity.Id, userEntity.Name, userEntity.Age);
        }

        public async Task<UserResponseDTO?> DeleteUserAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            
            if (user is null)
            {
                return null;
            }

            var userResponse = new UserResponseDTO(user.Id, user.Name, user.Age);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return userResponse;
        }
    }

}
