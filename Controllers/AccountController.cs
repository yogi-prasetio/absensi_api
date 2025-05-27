using System.Net;
using absensi_api.Data;
using absensi_api.Helper;
using absensi_api.Interface;
using absensi_api.Models;
using absensi_api.Repositories;
using absensi_api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace absensi_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IAccountRepository _accountRepository;
        public AccountController(AppDBContext context, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            var result = await _accountRepository.GetAll();

            return result is null || !result.Any() ? ApiResponseHelper.NotFound(message: "Account not found")
                : ApiResponseHelper.Ok(result, "All account retrieved");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            var result = await _accountRepository.GetById(id);

            return result is null ? ApiResponseHelper.NotFound(message: "Account not found")
                : ApiResponseHelper.Ok(result, "Account found");
        }

        [HttpPost]
        public async Task<ActionResult<AccountViewModel.CreateAccount>> Create(AccountViewModel.CreateAccount account)
        {
            if (account is null)
                return ApiResponseHelper.BadRequest("Request body can't be null");

            try
            {
                var result = await _accountRepository.Add(account);
                return result > 0
                ? ApiResponseHelper.Created(account, "Account added successfully")
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

        [HttpPut("{id}")]
        public async Task<ActionResult<object>> Update(string id, AccountViewModel.UpdateAccount request)
        {
            var result = await _accountRepository.Update(id, request);

            if (string.IsNullOrWhiteSpace(id))
                return ApiResponseHelper.BadRequest("Account ID is required");

            // if (request is null)
            //     return ApiResponseHelper.BadRequest("Request body can't be null");

            return result > 0
                ? ApiResponseHelper.Ok<object>("Account updated successfully")
                : ApiResponseHelper.BadRequest("Failed to update account");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            var result = await _accountRepository.Delete(id);

            return result > 0 ? ApiResponseHelper.NotFound(message: "Account not found")
                : ApiResponseHelper.Ok(result, "Account deleted successfully");
        }

    }
}