namespace WebAPI_TT1._1.DTOs
{
    public class APIResponse<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
