namespace absensi_api.ViewModels
{
    public class AttendanceLogViewModel
    {
        public class AttendanceLogDTO
        {
            public int AttendanceLogId { get; set; }
            public int AccountId { get; set; }
            public DateTime Date { get; set; }
            public int CheckInLogId { get; set; }
            public TimeSpan? TimeIn { get; set; }
            public required string PhotoIn { get; set; }
            public required string LocationIn { get; set; }
            public int CheckOutLogId { get; set; }
            public TimeSpan? TimeOut { get; set; }
            public string? PhotoOut { get; set; }
            public string? LocationOut { get; set; }
        }
        public class AttendanceRequest
        {
            public int account_id { get; set; }
            public DateTime date { get; set; }
            public TimeSpan time_in { get; set; }
            public required IFormFile photo { get; set; }
            public required string location { get; set; }
        }
        public class CreateAttendance
        {
            public int account_id { get; set; }
            public DateTime date { get; set; }
            public TimeSpan time_in { get; set; }
            public required string photo { get; set; }
            public required string location { get; set; }
        }
        public class UpdateAttendance
        {
            public DateTime date { get; set; }
        }
    }
}