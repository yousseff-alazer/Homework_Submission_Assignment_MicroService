using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Assignment.API.Assignment.CommonDefinitions.Responses
{
    
    public class BaseResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public int TotalCount { get; set; }
    }
}
