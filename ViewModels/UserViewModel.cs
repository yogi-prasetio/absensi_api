namespace absensi_api.ViewModels
{
    public class UserViewModel
    {
        public int nik { get; set; }
        public required string name { get; set; }
        public required string address { get; set; }
        public required string phone { get; set; }
        public required string email { get; set; }
        public required string location { get; set; }
    }
}