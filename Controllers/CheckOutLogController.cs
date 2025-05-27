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
    public class CheckOutLogController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly ICheckOutLogRepository _checkOutLogRepository;
        public CheckOutLogController(AppDBContext context, ICheckOutLogRepository checkOutLogRepository)
        {
            _checkOutLogRepository = checkOutLogRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckOutLog>>> GetAll()
        {
            var result = await _checkOutLogRepository.GetAll();

            return result is null || !result.Any() ? ApiResponseHelper.NotFound(message: "Check Out Log not found")
                : ApiResponseHelper.Ok(result, "All check out log retrieved");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CheckOutLog>> GetById(int id)
        {
            var result = await _checkOutLogRepository.GetById(id);

            return result is null ? ApiResponseHelper.NotFound(message: "Check Out Log not found")
                : ApiResponseHelper.Ok(result, "Check in log found");
        }

        [HttpPost]
        public async Task<ActionResult<CheckOutLogViewModel>> Create(CheckOutLogViewModel request)
        {
            if (request is null)
                return ApiResponseHelper.BadRequest("Request body can't be null");

            try
            {
                var result = await _checkOutLogRepository.Add(request);
                return result > 0
                ? ApiResponseHelper.Created(request, "Check Out Log added successfully")
                : ApiResponseHelper.BadRequest("Failed to add check out log");
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
        public async Task<ActionResult<object>> Update(string id, [FromForm] CheckOutLogViewModel.CheckOutRequest payload)
        {
            if (string.IsNullOrWhiteSpace(id))
                return ApiResponseHelper.BadRequest("Check Out Log ID is required");

            if (payload.photo is null || payload.photo.Length is 0)
                return BadRequest("File tidak ditemukan.");

            var ext = Path.GetExtension(payload.photo.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(ext))
                return BadRequest("Format file tidak didukung.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Chcek Out");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var dateStr = DateTime.Now.ToString("yyyyMMdd");
            var fileName = $"{payload.account_id}-check_out-{dateStr}{ext}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await payload.photo.CopyToAsync(stream);
            }

            var relativePath = Path.Combine("Uploads", "Check Out", fileName).Replace("\\", "/");

            var checkOutLog = new CheckOutLogViewModel
            {
                time_out = DateTime.Now.TimeOfDay,
                photo = relativePath,
                location = payload.location
            };

            var result = await _checkOutLogRepository.Update(id, checkOutLog);

            return result > 0
                ? ApiResponseHelper.Ok<object>("Check Out Log updated successfully")
                : ApiResponseHelper.BadRequest("Failed to update check out log");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var result = await _checkOutLogRepository.Delete(id);

            return result > 0 ? ApiResponseHelper.NotFound(message: "Account not found")
                : ApiResponseHelper.Ok(result, "Account deleted successfully");
        }
    }

}