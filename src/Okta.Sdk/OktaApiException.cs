using Okta.Sdk.Abstractions;

namespace Okta.Sdk
{
    public class OktaApiException : OktaException
    {
        private readonly Resource _resource = new Resource();

        public OktaApiException(int statusCode, Resource data)
            : base(message: data.GetStringProperty(nameof(ErrorSummary)))
        {
            StatusCode = statusCode;
            _resource = data;
        }

        public int StatusCode { get; }

        public string ErrorCode => _resource.GetStringProperty(nameof(ErrorCode));

        public string ErrorSummary => _resource.GetStringProperty(nameof(ErrorSummary));

        public string ErrorLink => _resource.GetStringProperty(nameof(ErrorLink));

        public string ErrorId => _resource.GetStringProperty(nameof(ErrorId));

        // TODO errorCauses (list of ?)
    }
}
