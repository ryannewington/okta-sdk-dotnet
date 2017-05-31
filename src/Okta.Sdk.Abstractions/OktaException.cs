using System;

namespace Okta.Sdk.Abstractions
{
    public class OktaException : Exception
    {
        public OktaException()
        {
        }

        public OktaException(string message)
            : base(message)
        {
        }

        public OktaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
