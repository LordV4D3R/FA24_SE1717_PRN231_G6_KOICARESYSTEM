namespace KoiCareSys.WebApp.Model
{
    public interface IBusinessResult<T>
    {
        int Status { get; set; }
        string? Message { get; set; }
        T? Data { get; set; }

    }

    public class BusinessResult<T> : IBusinessResult<T>
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public BusinessResult()
        {
            Status = -1;
            Message = "Action fail";
        }

        public BusinessResult(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public BusinessResult(int status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
