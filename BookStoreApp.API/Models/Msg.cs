
namespace BookStoreApp.API.Models
{
    public class Msg
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static Msg SuccessMsg(string message, object data = null) => new Msg
        {
            Success = true,
            Message = message,
            Data = data
        };

        public static Msg FailMsg(string message) => new Msg
        {
            Success = false,
            Message = message
        };
    }
}