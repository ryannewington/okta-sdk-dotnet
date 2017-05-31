using System;
using System.Collections.Generic;
using Okta.Sdk.Abstractions;

namespace Okta.Sdk
{
    public sealed class User : IResource
    {
        private readonly Resource _resource = new Resource();

        IChangeTrackingDictionary<string, object> IResource.GetData() =>
            _resource.GetData();

        IDictionary<string, object> IResource.GetModifiedData() =>
            _resource.GetModifiedData();

        void IResource.Initialize(IChangeTrackingDictionary<string, object> data) 
            => _resource.Initialize(data);

        public string Id => _resource.GetStringProperty(nameof(Id));

        public string Status => _resource.GetStringProperty(nameof(Status));

        public DateTimeOffset? Created => _resource.GetDateTimeProperty(nameof(Created));

        public DateTimeOffset? Activated => _resource.GetDateTimeProperty(nameof(Activated));

        public DateTimeOffset? StatusChanged => _resource.GetDateTimeProperty(nameof(StatusChanged));

        public DateTimeOffset? LastLogin => _resource.GetDateTimeProperty(nameof(LastLogin));

        public DateTimeOffset? LastUpdated => _resource.GetDateTimeProperty(nameof(LastUpdated));

        public DateTimeOffset? PasswordChanged => _resource.GetDateTimeProperty(nameof(PasswordChanged));

        public UserProfile Profile
        {
            get => _resource.GetProperty<UserProfile>(nameof(Profile));
            set => _resource.SetResourceProperty(nameof(Profile), value);
        }
    }
}
