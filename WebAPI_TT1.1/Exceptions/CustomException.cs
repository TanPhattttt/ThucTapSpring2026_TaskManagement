using System.Runtime.Serialization;

namespace WebAPI_TT1._1.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public CustomException()
        {
        }

        public CustomException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }


    }
}
