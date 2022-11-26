namespace GpProject206
{
    public class ErrorResponse
    {
        public string Error { get; set; } = "";

        public ErrorResponse(string error = "")
        {
            Error = error;
        }
    }
}