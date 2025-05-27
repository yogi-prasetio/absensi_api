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
    public class AttendanceLogController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IAttendanceLogRepository _attendanceLogRepository;
        public AttendanceLogController(AppDBContext context, IAttendanceLogRepository attendanceLogRepository)
        {
            _attendanceLogRepository = attendanceLogRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceLogViewModel.AttendanceLogDTO>>> GetAll()
        {
            var result = await _attendanceLogRepository.GetAll();

            return result is null || !result.Any() ? ApiResponseHelper.NotFound(message: "Attendance log not found")
                : ApiResponseHelper.Ok(result, "All attendance log retrieved");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceLogViewModel.AttendanceLogDTO>> GetById(int id)
        {
            var result = await _attendanceLogRepository.GetById(id);

            return result is null ? ApiResponseHelper.NotFound(message: "Account not found")
                : ApiResponseHelper.Ok(result, "Account found");
        }

        [HttpPost]
        public async Task<ActionResult<AttendanceLogViewModel.CreateAttendance>> Create([FromForm] AttendanceLogViewModel.AttendanceRequest payload)
        {
            if (payload is null)
                return ApiResponseHelper.BadRequest("Request body can't be null");

            if (payload.photo is null || payload.photo.Length is 0)
                return BadRequest("File not found");

            var ext = Path.GetExtension(payload.photo.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(ext))
                return BadRequest("Format file tidak didukung.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "CheckIn");
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

            var attendanceLog = new AttendanceLogViewModel.CreateAttendance
            {
                account_id = payload.account_id,
                date = DateTime.Now,
                time_in = DateTime.Now.TimeOfDay,
                photo = relativePath,
                location = payload.location
            };

            //Post data Attendance Log
            try
            {
                var result = await _attendanceLogRepository.Add(attendanceLog);
                return result > 0
                ? ApiResponseHelper.Created(attendanceLog, "Attendance log added successfully")
                : ApiResponseHelper.BadRequest("Failed to add Attendance log");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException is not null
                    ? $"{ex.Message} - {ex.InnerException.Message}"
                    : ex.Message;

                return ApiResponseHelper.BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<object>> Update(string id, AttendanceLogViewModel.UpdateAttendance request)
        {
            var result = await _attendanceLogRepository.Update(id, request);

            if (string.IsNullOrWhiteSpace(id))
                return ApiResponseHelper.BadRequest("Attendance Log ID is required");

            // if (request is null)
            //     return ApiResponseHelper.BadRequest("Request body can't be null");

            return result > 0
                ? ApiResponseHelper.Ok<object>("Attendance log updated successfully")
                : ApiResponseHelper.BadRequest("Failed to update attendance log");
        }
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<object>> Delete(int id)
        // {
        //     var result = await _attendanceLogRepository.Delete(id);

        //     return result > 0 ? ApiResponseHelper.NotFound(message: "Attendance log not found")
        //         : ApiResponseHelper.Ok(result, "Attendance log deleted successfully");
        // }

    }
}