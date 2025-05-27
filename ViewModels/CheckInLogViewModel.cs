namespace absensi_api.ViewModels
{
    public class CheckInLogViewModel
    {
        public TimeSpan? time_in { get; set; }
        public required string photo { get; set; }
        public required string location { get; set; }

        public class CheckInRequest
        {
            public int id { get; set; }
            public int account_id { get; set; }
            public TimeSpan time_in { get; set; }
            public required IFormFile photo { get; set; }
            public required string location { get; set; }
        }
    }
}