using Okta.Sdk.Abstractions;
using System.Collections.Generic;

namespace Okta.Sdk
{
    public class UserProfile : IResource
    {
        private readonly Resource _resource = new Resource();

        IChangeTrackingDictionary<string, object> IResource.GetData() =>
            _resource.GetData();

        IDictionary<string, object> IResource.GetModifiedData() =>
            _resource.GetModifiedData();

        void IResource.Initialize(IChangeTrackingDictionary<string, object> data)
            => _resource.Initialize(data);

        public string LastName
        {
            get => _resource.GetStringProperty(nameof(LastName));
            set => _resource.SetProperty(nameof(LastName), value);
        }

        public string Email
        {
            get => _resource.GetStringProperty(nameof(Email));
            set => _resource.SetProperty(nameof(Email), value);
        }

        public string Login
        {
            get => _resource.GetStringProperty(nameof(Login));
            set => _resource.SetProperty(nameof(Login), value);
        }

        public string FirstName
        {
            get => _resource.GetStringProperty(nameof(FirstName));
            set => _resource.SetProperty(nameof(FirstName), value);
        }

        public string DisplayName
        {
            get => _resource.GetStringProperty(nameof(DisplayName));
            set => _resource.SetProperty(nameof(DisplayName), value);
        }
    }
}
