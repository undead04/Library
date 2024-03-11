namespace Library.DTO
{
    public class BaseReponsitory<T>
    {
        public int StatusCode { get; set; } = 0;
        public T? Message { get; set; }
        public T? Data { get; set; }
        // Send Data
        public static BaseReponsitory<T> WithData(T? data,int statusCode)
        {
            return new BaseReponsitory<T> { StatusCode = statusCode, Data = data };
        }
        // Send Message
        public static BaseReponsitory<T> WithMessage(T message,int statusCode)
        {
            return new BaseReponsitory<T> { StatusCode = statusCode, Message = message };
        }
    }
}
