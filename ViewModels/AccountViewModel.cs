namespace absensi_api.ViewModels
{
    public class AccountViewModel
    {
        public class CreateAccount
        {
            public required string username { get; set; }
            public required string password { get; set; }
            public int user_id { get; set; }
            public int role_id { get; set; }
        }
        public class UpdateAccount
        {
            public required string password { get; set; }
            public int role_id { get; set; }
            public int? position_id { get; set; }
        }
    }
}