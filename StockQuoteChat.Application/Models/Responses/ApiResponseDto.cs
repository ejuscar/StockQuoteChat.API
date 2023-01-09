namespace StockQuoteChat.Application.Models.Responses
{
    public class ApiResponseDto<T>
    {
        public ApiResponseDto()
        {
            Data = default;
            Success = false;
            Error = string.Empty;
        }

        public ApiResponseDto(T data, bool success, string error)
        {
            Data = data;
            Success = success;
            Error = error;
        }

        public T? Data { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
