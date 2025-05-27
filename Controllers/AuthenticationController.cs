using System.Net;
using absensi_api.Data;
using absensi_api.Helper;
using absensi_api.Interface;
using absensi_api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace absensi_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationController(AppDBContext context, IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] AuthenticationViewModel.Login loginPayload)
        {
            try
            {
                var token = await _authenticationRepository.Login(loginPayload);
                if (token is null)
                    return ApiResponseHelper.Unauthorized("Email or Password is incorrect");

                return ApiResponseHelper.Ok(token, "Login successful");
            }
            catch (ExceptionHelper.CustomHttpException ex) when (ex.StatusCode == 403)
            {
                return ApiResponseHelper.Forbidden(ex.Message);
            }
            catch (ExceptionHelper.CustomHttpException ex)
            {
                return ApiResponseHelper.InternalServerError(ex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Unauthorized(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] AuthenticationViewModel.Register registerPayload)
        {
            var data = await _authenticationRepository.Register(registerPayload);
            if (data >= 1)
            {
                return StatusCode(200,
                    new
                    {
                        status = HttpStatusCode.OK,
                        message = "1 row inserted",
                        Data = data
                    });
            }
            else
            {
                return StatusCode(500,
                    new
                    {
                        status = HttpStatusCode.InternalServerError,
                        message = "Failed to enter data"
                    });
            }
        }
    }
}