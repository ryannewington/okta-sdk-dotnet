// <copyright file="UserProfile.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using Okta.Sdk.Abstractions;

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
