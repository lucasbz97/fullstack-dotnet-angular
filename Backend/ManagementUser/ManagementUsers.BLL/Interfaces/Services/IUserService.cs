using ManagementUsers.BLL.DTOs.Request.User;
using ManagementUsers.BLL.DTOs.Response.User;
using ManagementUsers.BLL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task<Response<UserResponse?>> CreateAsync(CreateUserRequest request);
        Task<Response<UserResponse?>> UpdateAsync(UpdateUserRequest request);
        Task<Response<UserResponse?>> DeleteAsync(DeleteUserRequest request);
        Task<Response<UserResponse?>> GetByIdAsync(GetUserByIdRequest request);
        Task<Response<List<UserResponse>?>> GetAllByUserIdAsync();
    }
}
