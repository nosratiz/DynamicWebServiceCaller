namespace WebServiceCaller.Common.General
{
    public class ApiMessage
    {
        public ApiMessage()
        {
        }

        public ApiMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}