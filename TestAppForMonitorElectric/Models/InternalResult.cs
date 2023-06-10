namespace TestAppForMonitorElectric.Models
{
    public record InternalResult<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
        public T? Model { get; set; }
    }
}
