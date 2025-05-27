using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using absensi_api.Data;
using absensi_api.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using absensi_api.ViewModels;
using absensi_api.Helper;
using absensi_api.Models;
using System.Security.Cryptography;

namespace absensi_api.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDBContext _context;
        public readonly IConfiguration _configuration;

        public AuthenticationRepository(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Task<string> GenerateToken(AuthenticationViewModel.Payload payload, string context)
        {
            try
            {
                var generateToken = JWTHelper.GenerateToken(payload, _configuration, context);
                return Task.FromResult(generateToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<string> Login(AuthenticationViewModel.Login loginPayload)
        {
            var account = await _context.Accounts
                .Where(c => c.username == loginPayload.username)
                .Select(c => new
                {
                    c.AccountId,
                    c.username,
                    c.password,
                    c.RoleId,
                    c.User,
                    c.Role
                })
                .FirstOrDefaultAsync();

            Console.WriteLine("Akun Name", account.username);
            if (account is null)
                throw new Exception("Account not registered.");

            if (!BcryptHelper.VerifyPassword(loginPayload.password, account.password))
                throw new Exception("The email or password you entered is incorrect.");

            // Create token payload
            var payload = new AuthenticationViewModel.Payload
            {
                nik = account.User.nik,
                role_id = account.RoleId,
                role_name = account.Role.role_name,
                name = account.User.name,
                email = account.User.email,
            };

            return await GenerateToken(payload, "Login");
        }

        public async Task<int> Register(AuthenticationViewModel.Register payload)
        {
            var user = new User
            {
                nik = payload.nik,
                name = payload.name,
                address = payload.address,
                phone = payload.phone,
                email = payload.email,
                image = null,
                location = payload.location,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(payload.password);
            var account = new Account
            {
                username = payload.email,
                password = hashPassword,
                RoleId = payload.role_id,
                UserId = user.UserId
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account.AccountId;
        }

        public async Task<bool> CheckDecryptedPayload(string data)
        {
            try
            {
                var decryptToken = EncryptionHelper.DecryptString(data, _configuration);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async Task<string> EncryptPayload(string data)
        {
            var encryptedToken = EncryptionHelper.EncryptString(data, _configuration);
            return await Task.FromResult(encryptedToken);
        }

        public async Task<string> DecryptPayload(string data)
        {
            try
            {
                var decryptToken = EncryptionHelper.DecryptString(data, _configuration);
                return await Task.FromResult(decryptToken);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // public async Task<Dictionary<string, object>> DecryptDictionaryPayload(Dictionary<string, object> data)
        // {
        //     // Before decrypting the dictionary, convert the JsonElement to its respective data type
        //     // Because the given decoded token that deserialized is still in JsonElement
        //     var convertedData = ConvertJsonElements(data);
        //     // After the dictionary is clean that only contain key-value pair of string-object
        //     // Then decrypt the dictionary
        //     var decryptDictionary = EncryptionHelper.DecryptDictionary(convertedData, _configuration);
        //     return await Task.FromResult(decryptDictionary);
        // }

        public Dictionary<string, object> ConvertJsonElements(Dictionary<string, object> data)
        {
            // Convert all JsonElement inside of dictionary to its respective data type
            var convertedDict = new Dictionary<string, object>();
            foreach (var kvp in data)
            {
                if (kvp.Value is JsonElement jsonElement)
                {
                    switch (jsonElement.ValueKind)
                    {
                        case JsonValueKind.String:
                            convertedDict[kvp.Key] = jsonElement.GetString();
                            break;
                        case JsonValueKind.Number:
                            if (jsonElement.TryGetInt32(out int intValue))
                            {
                                convertedDict[kvp.Key] = intValue;
                            }
                            else if (jsonElement.TryGetInt64(out long longValue))
                            {
                                convertedDict[kvp.Key] = longValue;
                            }
                            else if (jsonElement.TryGetDouble(out double doubleValue))
                            {
                                convertedDict[kvp.Key] = doubleValue;
                            }
                            break;
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                            convertedDict[kvp.Key] = jsonElement.GetBoolean();
                            break;
                        default:
                            convertedDict[kvp.Key] = kvp.Value;
                            break;
                    }
                }
                else
                {
                    convertedDict[kvp.Key] = kvp.Value;
                }
            }
            return convertedDict;
        }

        private string GenerateRandomBase64String(int length)
        {
            byte[] randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

    }
}