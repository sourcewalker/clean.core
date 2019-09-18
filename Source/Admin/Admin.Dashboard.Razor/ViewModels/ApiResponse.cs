using System.Dynamic;

namespace Admin.Dashboard.Razor.ViewModels
{
    public class ApiResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public ExpandoObject Data { get; set; }
    }
}
