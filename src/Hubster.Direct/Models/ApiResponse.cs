using System.Collections.Generic;
using System.Net;

namespace Hubster.Direct.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; internal set; }
        public List<ErrorCodeModel> Errors { get; internal set; }
    }

    public class ApiResponse<T> : ApiResponse where T: class
    {
        public T Content { get; internal set; }
    }
}
