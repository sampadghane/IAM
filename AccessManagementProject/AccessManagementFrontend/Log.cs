

namespace AccessManagementFrontend
{
    public class Log
    {
        public string User { get; set; }
        public string Action { get; set; }
        public string TimeStamp { get; set; }
    }

    public class LogResponse
    {
        public string Message { get; set; }
        public Log[] Logs { get; set; }
    }
}
