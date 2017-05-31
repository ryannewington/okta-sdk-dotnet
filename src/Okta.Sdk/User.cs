using System;
using System.Collections.Generic;
using Okta.Sdk.Abstractions;

namespace Okta.Sdk
{
    public sealed class User : IResource
    {
        private readonly Resource _resource = new Resource();

        IDictionary<string, object> IResource.GetModifiedData()
            => _resource.GetModifiedData();

        void IResource.Initialize(IChangeTrackingDictionary<string, object> data)
            => _resource.Initialize(data);

        public string Id
        {
            get => _resource.GetStringProperty(nameof(Id));
            set => _resource.SetProperty(nameof(Id), value);
        }

        public string Status
        {
            get => _resource.GetStringProperty(nameof(Status));
            set => _resource.SetProperty(nameof(Status), value);
        }
    }
}
