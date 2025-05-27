using System.Net;
using absensi_api.Data;
using absensi_api.Models;
using absensi_api.Interface;
using absensi_api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using absensi_api.Helper;

namespace absensi_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CheckInLogController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly ICheckInLogRepository _checkInLogRepository;
        public CheckInLogController(AppDBContext context, ICheckInLogRepository checkInLogRepository)
        {
            _checkInLogRepository = checkInLogRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckInLog>>> GetAll()
        {
            var result = await _checkInLogRepository.GetAll();

            return result is null || !result.Any() ? ApiResponseHelper.NotFound(message: "Check In Log not found")
                : ApiResponseHelper.Ok(result, "All check in log retrieved");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CheckInLog>> GetById(int id)
        {
            var result = await _checkInLogRepository.GetById(id);

            return result is null ? ApiResponseHelper.NotFound(message: "Check In Log not found")
                : ApiResponseHelper.Ok(result, "Check in log found");
        }

        [HttpPost]
        public async Task<ActionResult<CheckInLogViewModel>> Create(CheckInLogViewModel request)
        {
            if (request is null)
                return ApiResponseHelper.BadRequest("Request body can't be null");

            try
            {
                var result = await _checkInLogRepository.Add(request);
                return result > 0
                ? ApiResponseHelper.Created(request, "Check In Log added successfully")
                : ApiResponseHelper.BadRequest("Failed to add check in log");
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
        public async Task<ActionResult<object>> Update(string id, [FromForm] CheckInLogViewModel.CheckInRequest payload)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ApiResponseHelper.BadRequest("Check In Log ID is required");

            if (payload.photo is null || payload.photo.Length is 0)
                return BadRequest("File tidak ditemukan.");

            var ext = Path.GetExtension(payload.photo.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(ext))
                return BadRequest("Format file tidak didukung.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "ChcekIn");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var dateStr = DateTime.Now.ToString("yyyyMMdd");
            var fileName = $"{payload.account_id}-check_in-{dateStr}{ext}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await payload.photo.CopyToAsync(stream);
            }

            var relativePath = Path.Combine("Uploads", "CheckIn", fileName).Replace("\\", "/");

            var checkInLog = new CheckInLogViewModel
            {
                time_in = DateTime.Now.TimeOfDay,
                photo = relativePath,
                location = payload.location
            };

            var result = await _checkInLogRepository.Update(id, checkInLog);

            return result > 0
                ? ApiResponseHelper.Ok<object>("Check In Log updated successfully")
                : ApiResponseHelper.BadRequest("Failed to update check in log");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var result = await _checkInLogRepository.Delete(id);

            return result > 0 ? ApiResponseHelper.NotFound(message: "Account not found")
                : ApiResponseHelper.Ok(result, "Account deleted successfully");
        }
    }

}