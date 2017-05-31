using System.Collections.Generic;

namespace Okta.Sdk.Abstractions
{
    public class HttpResponse
    {
        public int StatusCode { get; set; }

        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }
    }

    public class HttpResponse<T> : HttpResponse
    {
        public T Payload { get; set; }
    }
}