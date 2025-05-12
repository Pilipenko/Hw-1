
namespace MvcAspAzure.Application.Common {
    public sealed  class ServiceResult<T> {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };
        public static ServiceResult<T> Fail(params string[] errors) => new() { Success = false, Errors = errors.ToList() };

    }

    public sealed class ServiceResult {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();

        public static ServiceResult Ok() => new() { Success = true };
        public static ServiceResult Fail(params string[] errors) => new() { Success = false, Errors = errors.ToList() };

    }
}
