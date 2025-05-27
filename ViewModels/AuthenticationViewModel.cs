using System.ComponentModel.DataAnnotations;

namespace absensi_api.ViewModels
{
    public class AuthenticationViewModel
    {
        public class Login
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid Email Address.")]
            public required string username { get; set; }
            public required string password { get; set; }
        }
        public class Register
        {
            [Required(ErrorMessage = "NIK is required.")]
            public int nik { get; set; }
            [Required(ErrorMessage = "Name is required.")]
            public required string name { get; set; }
            [Required(ErrorMessage = "Address is required.")]
            public required string address { get; set; }
            [Required(ErrorMessage = "Phone is required.")]
            public required string phone { get; set; }
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid Email Address.")]
            public required string email { get; set; }
            [Required(ErrorMessage = "Location is required.")]
            public required string location { get; set; }
            [Required(ErrorMessage = "Role is required.")]
            public int role_id { get; set; }
            [Required(ErrorMessage = "Password is required.")]
            public required string password { get; set; }
        }

        public class Validation
        {
            public required string token { get; set; }
        }

        public class Payload
        {
            public int account_id { get; set; }
            public int role_id { get; set; }
            public required string role_name { get; set; }
            public int nik { get; set; }
            public required string name { get; set; }
            public required string email { get; set; }
        }
    }
}