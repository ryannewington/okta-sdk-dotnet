using System.Collections.Generic;

namespace Okta.Sdk.Abstractions
{
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1649 // File name must match first type name
    public class HttpResponse
    {
        public int StatusCode { get; set; }

        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }
    }

    public class HttpResponse<T> : HttpResponse
    {
        public T Payload { get; set; }
    }
#pragma warning restore SA1649 // File name must match first type name
#pragma warning restore SA1402 // File may only contain a single type
}