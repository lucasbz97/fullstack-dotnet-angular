using ManagementUsers.BLL.DTOs.Request.DependentRequest;
using ManagementUsers.BLL.DTOs.Response;
using ManagementUsers.BLL.DTOs.Response.Dependent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.Interfaces.Services
{
    public interface IDependentService
    {
        Task<Response<List<DependentResponse>?>> CreateAsync(CreateDependentRequest request);
        Task<Response<List<DependentResponse>?>> UpdateAsync(List<UpdateDependentRequest> request);
        Task<Response<DependentResponse?>> DeleteAsync(DeleteDependentRequest request);
        Task<Response<List<DependentResponse>>?> GetAllByUserIdAsync(GetAllDependentRequest request);
    }
}
