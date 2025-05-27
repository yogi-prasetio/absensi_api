using System.Net;
using absensi_api.Data;
using absensi_api.Models;
using absensi_api.Interface;
using absensi_api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using absensi_api.Helper;

namespace absensi_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IUserRepository _userRepository;
        public UserController(AppDBContext context, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var result = await _userRepository.GetAll();

            return result is null || !result.Any() ? ApiResponseHelper.NotFound(message: "User not found")
                : ApiResponseHelper.Ok(result, "All user retrieved");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var result = await _userRepository.GetById(id);

            return result is null ? ApiResponseHelper.NotFound(message: "User not found")
                : ApiResponseHelper.Ok(result, "User found");
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Create(UserViewModel user)
        {
            if (user is null)
                return ApiResponseHelper.BadRequest("Request body can't be null");

            try
            {
                var result = await _userRepository.Add(user);
                return result > 0
                ? ApiResponseHelper.Created(user, "Account added successfully")
                : ApiResponseHelper.BadRequest("Failed to add account");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException is not null
                    ? $"{ex.Message} - {ex.InnerException.Message}"
                    : ex.Message;

                return ApiResponseHelper.BadRequest(errorMessage);
            }
        }

        [HttpPut]
        public async Task<ActionResult<object>> Update(string id, UserViewModel request)
        {
            var result = await _userRepository.Update(id, request);

            if (string.IsNullOrWhiteSpace(id))
                return ApiResponseHelper.BadRequest("Account ID is required");

            // if (request is null)
            //     return ApiResponseHelper.BadRequest("Request body can't be null");

            return result > 0
                ? ApiResponseHelper.Ok<object>("User updated successfully")
                : ApiResponseHelper.BadRequest("Failed to update user");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var result = await _userRepository.Delete(id);

            return result > 0 ? ApiResponseHelper.NotFound(message: "User not found")
                : ApiResponseHelper.Ok(result, "User deleted successfully");
        }

    }
}