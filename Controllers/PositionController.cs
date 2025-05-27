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
    public class PositionController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IPositionRepository _positionRepository;
        public PositionController(AppDBContext context, IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetAll()
        {
            var result = await _positionRepository.GetAll();

            return result is null || !result.Any() ? ApiResponseHelper.NotFound(message: "Position not found")
                : ApiResponseHelper.Ok(result, "All position retrieved");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetById(int id)
        {
            var result = await _positionRepository.GetById(id);

            return result is null ? ApiResponseHelper.NotFound(message: "Position not found")
                : ApiResponseHelper.Ok(result, "Position found");
        }

        [HttpPost]
        public async Task<ActionResult<PositionViewModel>> Create(PositionViewModel position)
        {
            if (position is null)
                return ApiResponseHelper.BadRequest("Request body can't be null");

            try
            {
                var result = await _positionRepository.Add(position);
                return result > 0
                ? ApiResponseHelper.Created(position, "Account added successfully")
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
        public async Task<ActionResult<object>> Update(string id, PositionViewModel request)
        {
            var result = await _positionRepository.Update(id, request);

            if (string.IsNullOrWhiteSpace(id))
                return ApiResponseHelper.BadRequest("Account ID is required");

            // if (request is null)
            //     return ApiResponseHelper.BadRequest("Request body can't be null");

            return result > 0
                ? ApiResponseHelper.Ok<object>("Position updated successfully")
                : ApiResponseHelper.BadRequest("Failed to update position");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var result = await _positionRepository.Delete(id);

            return result > 0 ? ApiResponseHelper.NotFound(message: "Position not found")
                : ApiResponseHelper.Ok(result, "Position deleted successfully");
        }

    }
}