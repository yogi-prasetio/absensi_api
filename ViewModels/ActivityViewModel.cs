namespace absensi_api.ViewModels
{
    public class ActivityViewModel
    {
        public class CreateActivity
        {
            public required string activity { get; set; }
            public TimeSpan start_time { get; set; }
            public TimeSpan end_time { get; set; }
            public int attendance_log_id { get; set; }
        }
        public class UpdateActivity
        {
            public required string activity { get; set; }
            public TimeSpan start_time { get; set; }
            public TimeSpan end_time { get; set; }
        }
    }
}