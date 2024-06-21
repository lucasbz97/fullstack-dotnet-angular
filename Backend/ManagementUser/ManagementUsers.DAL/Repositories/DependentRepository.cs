using Azure.Core;
using ManagementUsers.DAL.Contexts;
using ManagementUsers.DAL.DTOs.Request;
using ManagementUsers.DAL.DTOs.Response;
using ManagementUsers.DAL.Entities;
using ManagementUsers.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementUsers.DAL.Repositories
{
    public class DependentRepository : IDependentRepository
    {
        private readonly AppDbContext _context;

        public DependentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DependentResponseDTO> GetDependentByIdAsync(long id)
        {
            var dependentEntity =  await _context.Dependents.FindAsync(id);

            return new DependentResponseDTO(dependentEntity.Id, dependentEntity.Name, dependentEntity.Age, dependentEntity.UserId);
        }

        public async Task AddDependentAsync(DependentRequestDTO dependentDTO)
        {
            var dependentEntity = new DependentModel(dependentDTO.Id, dependentDTO.Name, dependentDTO.Age, dependentDTO.UserId);
            await _context.Dependents.AddAsync(dependentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DependentResponseDTO>> GetAllDependentsAsync(long userId)
        {
            var query = _context.Dependents
            .AsNoTracking()
                .Where(dependent => dependent.UserId == userId)
                .OrderBy(dependent => dependent.Name);

            var result = await query.ToListAsync();

            if (result.Count == 0)
                return null;

            var response = result.Select(dep => new DependentResponseDTO(dep.Id, dep.Name, dep.Age, dep.UserId));

            return response;
        }

        public async Task<DependentResponseDTO?> UpdateDependentAsync(DependentRequestDTO dependentDTO)
        {
            var dependentEntity = await _context.Dependents.FindAsync(dependentDTO.Id);

            if (dependentEntity is null)
                return null;

            dependentEntity.Age = dependentDTO.Age;
            dependentDTO.Name = dependentDTO.Name;

            _context.Dependents.Update(dependentEntity);
            await _context.SaveChangesAsync();

            return new DependentResponseDTO(dependentEntity.Id, dependentEntity.Name, dependentEntity.Age, dependentEntity.UserId);
        }

        public async Task<DependentResponseDTO?> DeleteDependentAsync(long id)
        {
            var dependent = await _context.Dependents.FindAsync(id);
            if (dependent == null)
                return null;

            _context.Dependents.Remove(dependent);
            await _context.SaveChangesAsync();
            return new DependentResponseDTO(dependent.Id, dependent.Name, dependent.Age, dependent.UserId);
        }
    }

}
