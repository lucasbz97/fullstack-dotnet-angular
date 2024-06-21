using ManagementUsers.BLL.Configuration;
using ManagementUsers.BLL.DTOs.Request.DependentRequest;
using ManagementUsers.BLL.DTOs.Request.User;
using ManagementUsers.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManagementUsers.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDependentService _dependentService;

        public UserController(IUserService userService, IDependentService dependentService)
        {
            _userService = userService;
            _dependentService = dependentService;
        }

        [HttpPost]
        public async Task<IResult> CreateAsync(CreateUserRequest request)
        {
            var response = await _userService.CreateAsync(request);

            request.Dependent.UserId = response.Data.Id;

            var dependentResponse = await _dependentService.CreateAsync(request.Dependent);

            if (dependentResponse != null && dependentResponse.Data != null)
                response.Data.Dependents = dependentResponse.Data;

            return response.IsSuccess
                ? TypedResults.Created($"api/user", response)
                : TypedResults.BadRequest(response);
        }

        [HttpDelete("/api/user/{id:long}")]
        public async Task<IResult> DeleteAsync([FromRoute] long id)
        {
            var requestDependent = new DeleteDependentRequest
            {
                UserId = id
            };
            var dependentResponse = await _dependentService.DeleteAsync(requestDependent);

            var requestUser = new DeleteUserRequest
            {
                Id = id
            };
            var response = await _userService.DeleteAsync(requestUser);

            if (dependentResponse != null && dependentResponse.Data != null)
                response.Data.Dependents.Add(dependentResponse.Data);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }

        [HttpGet]
        public async Task<IResult> GetAllAsync()
        {
            var response = await _userService.GetAllByUserIdAsync();

            if (response is null)
                return TypedResults.Ok(response);

            foreach (var item in response.Data)
            {
                var dependentRequest = new GetAllDependentRequest
                {
                    UserId = item.Id
                };

                var dependentResponse = await _dependentService.GetAllByUserIdAsync(dependentRequest);

                if (dependentResponse != null && dependentResponse.Data != null)
                    item.Dependents = dependentResponse.Data;
            }

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }

        [HttpGet("/api/user/{id:long}")]
        public async Task<IResult> GetByIdAsync(long id)
        {
            var request = new GetUserByIdRequest
            {
                Id = id
            };
            var response = await _userService.GetByIdAsync(request);

            var requestDependent = new GetAllDependentRequest
            {
                UserId = id
            };

            var dependentResponse = await _dependentService.GetAllByUserIdAsync(requestDependent);

            if (dependentResponse != null && dependentResponse.Data != null)
                response.Data.Dependents = dependentResponse.Data;

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }

        [HttpPut("/api/user/{id:long}")]
        public async Task<IResult> UpdateAsync(IUserService userService, long id, UpdateUserRequest request)
        {
            request.Id = id;
            var response = await userService.UpdateAsync(request);

            foreach (var item in request.Dependents)
            {
                item.UserId = id;
            }
            var dependentResponse = await _dependentService.UpdateAsync(request.Dependents);

            if (dependentResponse != null && dependentResponse.Data != null)
                response.Data.Dependents = dependentResponse.Data;

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }
    }
}
