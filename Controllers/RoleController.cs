using System.Net;
using absensi_api.Data;
using absensi_api.Helper;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace absensi_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IRoleRepository _roleRepository;
        public RoleController(AppDBContext context, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll()
        {
            var result = await _roleRepository.GetAll();

            return result is null || !result.Any() ? ApiResponseHelper.NotFound(message: "Role not found")
                : ApiResponseHelper.Ok(result, "All role retrieved");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(int id)
        {
            var result = await _roleRepository.GetById(id);

            return result is null ? ApiResponseHelper.NotFound(message: "Role not found")
                : ApiResponseHelper.Ok(result, "Role found");
        }

        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> Create(RoleViewModel role)
        {
            if (role is null)
                return ApiResponseHelper.BadRequest("Request body can't be null");

            try
            {
                var result = await _roleRepository.Add(role);
                return result > 0
                ? ApiResponseHelper.Created(role, "Account added successfully")
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
        public async Task<ActionResult<object>> Update(string id, RoleViewModel request)
        {
            var result = await _roleRepository.Update(id, request);

            if (string.IsNullOrWhiteSpace(id))
                return ApiResponseHelper.BadRequest("Account ID is required");

            // if (request is null)
            //     return ApiResponseHelper.BadRequest("Request body can't be null");

            return result > 0
                ? ApiResponseHelper.Ok<object>("Role updated successfully")
                : ApiResponseHelper.BadRequest("Failed to update role");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var result = await _roleRepository.Delete(id);

            return result > 0 ? ApiResponseHelper.NotFound(message: "Role not found")
                : ApiResponseHelper.Ok(result, "Role deleted successfully");
        }

    }
}