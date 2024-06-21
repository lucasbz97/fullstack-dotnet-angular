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
    public interface IDependentRepository
    {
        Task<DependentResponseDTO> GetDependentByIdAsync(long id);
        Task AddDependentAsync(DependentRequestDTO dependent);
        Task<IEnumerable<DependentResponseDTO>> GetAllDependentsAsync(long userId);
        Task<DependentResponseDTO?> UpdateDependentAsync(DependentRequestDTO dependent);
        Task<DependentResponseDTO?> DeleteDependentAsync(long id);
    }
}
