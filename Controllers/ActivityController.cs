using System.Diagnostics;
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
    public class ActivityController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IActivityRepository _activityRepository;
        public ActivityController(AppDBContext context, IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Activity>>> GetAll()
        {
            var result = await _activityRepository.GetAll();

            return result is null || !result.Any() ? ApiResponseHelper.NotFound(message: "Activity not found")
                : ApiResponseHelper.Ok(result, "All activity retrieved");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Activity>> GetById(int id)
        {
            var result = await _activityRepository.GetById(id);

            return result is null ? ApiResponseHelper.NotFound(message: "Activity not found")
                : ApiResponseHelper.Ok(result, "Activity found");
        }

        [HttpPost]
        public async Task<ActionResult<ActivityViewModel.CreateActivity>> Create(ActivityViewModel.CreateActivity activity)
        {
            if (activity is null)
                return ApiResponseHelper.BadRequest("Request body can't be null");

            try
            {
                var result = await _activityRepository.Add(activity);
                return result > 0
                ? ApiResponseHelper.Created(activity, "Account added successfully")
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
        public async Task<ActionResult<object>> Update(string id, ActivityViewModel.UpdateActivity request)
        {
            var result = await _activityRepository.Update(id, request);

            if (string.IsNullOrWhiteSpace(id))
                return ApiResponseHelper.BadRequest("Account ID is required");

            // if (request is null)
            //     return ApiResponseHelper.BadRequest("Request body can't be null");

            return result > 0
                ? ApiResponseHelper.Ok<object>("Activity updated successfully")
                : ApiResponseHelper.BadRequest("Failed to update activity");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var result = await _activityRepository.Delete(id);

            return result > 0 ? ApiResponseHelper.NotFound(message: "Activity not found")
                : ApiResponseHelper.Ok(result, "Activity deleted successfully");
        }

    }
}