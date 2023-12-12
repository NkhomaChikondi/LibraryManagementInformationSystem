namespace LMIS.Api.Core.DTOs
{
    public class BaseResponse<T>
    {
        public bool IsError {  get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
