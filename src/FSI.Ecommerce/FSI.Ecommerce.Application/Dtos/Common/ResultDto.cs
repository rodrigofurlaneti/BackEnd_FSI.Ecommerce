namespace FSI.Ecommerce.Application.Dtos.Common
{
    public sealed class ResultDto<T>
    {
        public bool Success { get; }
        public string? Error { get; }
        public T? Data { get; }

        private ResultDto(bool success, T? data, string? error)
        {
            Success = success;
            Data = data;
            Error = error;
        }

        public static ResultDto<T> Ok(T data) => new(true, data, null);
        public static ResultDto<T> Fail(string error) => new(false, default, error);
    }
}