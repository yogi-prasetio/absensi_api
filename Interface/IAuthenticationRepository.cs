
using absensi_api.ViewModels;

namespace absensi_api.Interface
{
    public interface IAuthenticationRepository
    {
        Task<String> Login(AuthenticationViewModel.Login loginViewModel);
        Task<int> Register(AuthenticationViewModel.Register registerViewModel);
        Task<String> GenerateToken(AuthenticationViewModel.Payload payload, string context);

        Task<Boolean> CheckDecryptedPayload(string data);

        Task<String> DecryptPayload(string data);

        // Task<Dictionary<string, object>> DecryptDictionaryPayload(Dictionary<string, object> data);

        Dictionary<string, object> ConvertJsonElements(Dictionary<string, object> data);
    }
}