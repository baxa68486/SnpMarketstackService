using System;

namespace SnpMarketstackService.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
    }
}
