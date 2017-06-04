// <copyright file="User.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace Okta.Sdk
{
    public sealed class User : Resource
    {
        public User()
            : base(ResourceDictionaryType.ChangeTracking)
        {
        }

        //IChangeTrackingDictionary<string, object> IResource.GetData() =>
        //    GetData();

        //IDictionary<string, object> IResource.GetModifiedData() =>
        //    GetModifiedData();

        //void IResource.Initialize(IChangeTrackingDictionary<string, object> data)
        //    => Initialize(data);

        public string Id => GetStringProperty(nameof(Id));

        public string Status => GetStringProperty(nameof(Status));

        public DateTimeOffset? Created => GetDateTimeProperty(nameof(Created));

        public DateTimeOffset? Activated => GetDateTimeProperty(nameof(Activated));

        public DateTimeOffset? StatusChanged => GetDateTimeProperty(nameof(StatusChanged));

        public DateTimeOffset? LastLogin => GetDateTimeProperty(nameof(LastLogin));

        public DateTimeOffset? LastUpdated => GetDateTimeProperty(nameof(LastUpdated));

        public DateTimeOffset? PasswordChanged => GetDateTimeProperty(nameof(PasswordChanged));

        public UserProfile Profile
        {
            get => GetProperty<UserProfile>(nameof(Profile));
            set => SetResourceProperty(nameof(Profile), value);
        }
    }
}
