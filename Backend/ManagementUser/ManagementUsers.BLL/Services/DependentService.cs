using AutoMapper;
using ManagementUsers.BLL.Domain;
using ManagementUsers.BLL.DTOs.Request.DependentRequest;
using ManagementUsers.BLL.DTOs.Response;
using ManagementUsers.BLL.DTOs.Response.Dependent;
using ManagementUsers.BLL.Interfaces.Services;
using ManagementUsers.DAL.Contexts;
using ManagementUsers.DAL.DTOs.Request;
using ManagementUsers.DAL.DTOs.Response;
using ManagementUsers.DAL.Interfaces;
using ManagementUsers.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ManagementUsers.BLL.Services
{
    public class DependentService : IDependentService
    {
        private readonly IDependentRepository _dependentRepository;
        private readonly ILogger<DependentService> _logger;
        private readonly IMapper _mapper;
        public DependentService(IDependentRepository dependentRepository, ILogger<DependentService> logger, IMapper mapper)
        {
            _dependentRepository = dependentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<List<DependentResponse>?>> CreateAsync(CreateDependentRequest request)
        {
            if (request.DependentsData.IsNullOrEmpty())
            {
                _logger.LogInformation($"Nao possui dependentes");
                return new Response<List<DependentResponse>?>(null, (int)HttpStatusCode.OK, "Nao possui dependentes");
            }

            try
            {
                var dependentModelList = request.DependentsData
                        .Select(dependentData => _mapper.Map<ManagementUsers.DAL.DTOs.Request.DependentRequestDTO >
                        (
                            new DependentModel(dependentData.Name, dependentData.Age, request.UserId)
                        ))
                        .ToList();
                foreach (ManagementUsers.DAL.DTOs.Request.DependentRequestDTO dependent in dependentModelList) 
                {
                    await _dependentRepository.AddDependentAsync(dependent);
                }

                var result = dependentModelList
                        .Select(dependentData => new DependentResponse(dependentData.Id, dependentData.Name, dependentData.Age))
                        .ToList();

                return new Response<List<DependentResponse>?>(result, (int)HttpStatusCode.Created, "Dependente criado com sucesso !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar Dependente no metodo CreateAsync. Detalhes: {ex.StackTrace}");
                return new Response<List<DependentResponse>?>(null, 500, "O Processo de adicionar dependente falhou !");
            }
        }

        public async Task<Response<DependentResponse?>> DeleteAsync(DeleteDependentRequest request)
        {
            try
            {
                var dependent = await _dependentRepository.DeleteDependentAsync(request.Id);

                if (dependent is null)
                {
                    return new Response<DependentResponse?>(null, (int)HttpStatusCode.NotFound, "Dependente não encontrado !");
                }

                var result = new DependentResponse(dependent.Id, dependent.Name, dependent.Age);

                return new Response<DependentResponse?>(result, (int)HttpStatusCode.OK, "Dependete removido com sucesso !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao remover dependente no metodo DeleteAsync. Detalhes: {ex.StackTrace}");
                return new Response<DependentResponse?>(null, (int)HttpStatusCode.InternalServerError, "O Processo de deletar dependente falhou !");
            }
        }

        public async Task<Response<List<DependentResponse>>?> GetAllByUserIdAsync(GetAllDependentRequest request)
        {
            try
            {
                var dependents = await _dependentRepository.GetAllDependentsAsync(request.UserId);

                var result = dependents
                        .Select(dependentData => new DependentResponse(dependentData.Id, dependentData.Name, dependentData.Age))
                        .ToList();

                return new Response<List<DependentResponse>>(result, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao pegar lista de dependentes por usuario no metodo GetAllAsync. Detalhes: {ex.StackTrace}");
                return new Response<List<DependentResponse>>(null, (int)HttpStatusCode.InternalServerError, "O Processo de pegar a lista de dependentes de usuario falhou !");
            }
        }

        public async Task<Response<List<DependentResponse>?>> UpdateAsync(List<UpdateDependentRequest> request)
        {
            try
            {
                var dependencies = await _dependentRepository.GetAllDependentsAsync(request[0].UserId);
                
                var existingDependencies = dependencies.Where(d => request.Any(r => r.Id == d.Id))
                    .Select(dep => new DependentRequestDTO(dep.Id, dep.Name, dep.Age, dep.UserId))
                    .ToList();
                var newDependencies = request.Where(r => dependencies.All(d => d.Id != r.Id))
                    .Select(dep => new DependentRequestDTO(dep.Name, dep.Age, dep.UserId))
                    .ToList();
                var removedDependencies = dependencies.Where(d => request.All(r => r.Id != d.Id))
                    .Select(dep => new DependentRequestDTO(dep.Id, dep.Name, dep.Age, dep.UserId))
                    .ToList();

                await CreateDependency(newDependencies);
                await DeleteDependency(removedDependencies);

                var result = new List<DependentResponse?>();
                foreach (var depRequest in existingDependencies)
                {
                    var dependent = request.First(dep => dep.Id == depRequest.Id);

                    depRequest.Age = dependent.Age;
                    depRequest.Name = dependent.Name;
                    
                    var dependentUpdated = await _dependentRepository.UpdateDependentAsync(depRequest);

                    result.Add(new DependentResponse(dependentUpdated.Id, dependentUpdated.Name, dependentUpdated.Age));
                }

                return new Response<List<DependentResponse>?>(result, (int)HttpStatusCode.OK, "Dependente atualizado com sucesso !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar dependente no metodo UpdateAsync. Detalhes: {ex.StackTrace}");
                return new Response<List<DependentResponse>?>(null, (int)HttpStatusCode.InternalServerError, "O Processo de atualizar dependente falhou !");
            }
        }

        private async Task DeleteDependency(List<DependentRequestDTO> removedDependencies)
        {
            if (removedDependencies.IsNullOrEmpty())
                return;

            foreach (var dependency in removedDependencies)
            {
                await _dependentRepository.DeleteDependentAsync(dependency.Id);
            }
        }

        private async Task CreateDependency(List<DependentRequestDTO> newDependencies)
        {
            if (newDependencies.IsNullOrEmpty())
                return;

            foreach (var dependency in newDependencies)
            {
                var request = new DependentRequestDTO(dependency.Name, dependency.Age, dependency.UserId);
                await _dependentRepository.AddDependentAsync(request);
            }
        }
    }
}
