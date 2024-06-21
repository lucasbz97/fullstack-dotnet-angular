using ManagementUsers.BLL.Domain;
using ManagementUsers.BLL.DTOs.Request.User;
using ManagementUsers.BLL.DTOs.Response.User;
using ManagementUsers.BLL.DTOs.Response;
using ManagementUsers.BLL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ManagementUsers.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace ManagementUsers.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<UserResponse?>> CreateAsync(CreateUserRequest request)
        {
            try
            {
                var user = new UserModel(request.Name, request.Age, DateTime.UtcNow);

                var userDTO = _mapper.Map<ManagementUsers.DAL.DTOs.Request.UserRequestDTO>(user);

                var userCreated = await _userRepository.AddUserAsync(userDTO);

                var result = new UserResponse(userCreated.Name, userCreated.Age, userCreated.Id);

                return new Response<UserResponse?>(result, (int)HttpStatusCode.Created, "Usuario criado com sucesso !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao criar usuario no metodo CreateAsync. Detalhes: {ex.StackTrace}");
                return new Response<UserResponse?>(null, 500, "O Processo de criar usuario falhou !");
            }
        }

        public async Task<Response<UserResponse?>> DeleteAsync(DeleteUserRequest request)
        {
            try
            {
                var user = await _userRepository.DeleteUserAsync(request.Id);

                //var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Id == request.Id);

                if (user is null)
                {
                    return new Response<UserResponse?>(null, (int)HttpStatusCode.NotFound, "Usuario não encontrado !");
                }

                var result = new UserResponse(user.Name, user.Age, user.Id);

                return new Response<UserResponse?>(result, (int)HttpStatusCode.OK, "Usuario removido com sucesso !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao deletar usuario no metodo DeleteAsync. Detalhes: {ex.StackTrace}");
                return new Response<UserResponse?>(null, (int)HttpStatusCode.InternalServerError, "O Processo de deletar usuario falhou !");
            }
        }

        public async Task<Response<UserResponse?>> GetByIdAsync(GetUserByIdRequest request)
        {
            try
            {

                var user = await _userRepository.GetUserByIdAsync(request.Id);

                if (user is null)
                {
                    return new Response<UserResponse?>(null, (int)HttpStatusCode.NotFound, "Usuario não encontrado !");
                }

                var result = new UserResponse(user.Name, user.Age, user.Id);

                return new Response<UserResponse?>(result, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao pegar usuario por Id no metodo GetByIdAsync. Detalhes: {ex.StackTrace}");
                return new Response<UserResponse?>(null, (int)HttpStatusCode.InternalServerError, "O Processo de pegar usuario por Id falhou !");
            }
        }

        public async Task<Response<UserResponse?>> UpdateAsync(UpdateUserRequest request)
        {
            try
            {
                var user = new UserModel(request.Id, request.Name, request.Age, DateTime.UtcNow);
                var userEntity = _mapper.Map<ManagementUsers.DAL.DTOs.Request.UserRequestDTO>(user);

                if (user is null)
                {
                    return new Response<UserResponse?>(null, (int)HttpStatusCode.NotFound, "Usuario não encontrado !");
                }

                await _userRepository.UpdateUserAsync(userEntity);

                var result = new UserResponse(userEntity.Name, userEntity.Age, userEntity.Id);

                return new Response<UserResponse?>(result, (int)HttpStatusCode.OK, "Usuario atualizado com sucesso !");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar usuario no metodo UpdateAsync. Detalhes: {ex.StackTrace}");
                return new Response<UserResponse?>(null, (int)HttpStatusCode.InternalServerError, "O Processo de atualizar usuario falhou !");
            }
        }

        public async Task<Response<List<UserResponse>?>> GetAllByUserIdAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                
                if (users.IsNullOrEmpty())
                    return new Response<List<UserResponse>?>(null, (int)HttpStatusCode.NotFound, "Nenhum usuario cadastrado!");

                var count = users.Count();

                var result = users
                        .Select(user => new UserResponse(user.Name, user.Age, user.Id))
                        .ToList();

                return new Response<List<UserResponse>?>(result, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao pegar list de usuarios no metodo GetAllAsync. Detalhes: {ex.StackTrace}");
                return new Response<List<UserResponse>?>(null, (int)HttpStatusCode.InternalServerError, "O Processo de pegar a lista de usuarios falhou !");
            }
        }
    }
}
