using System.Net;

namespace Hubster.Auth
{
    public class Response
    {
        public HttpStatusCode ServerStatusCode { get; internal set; }
        public string ServerStatusMessage { get; internal set; }        
    }
}
