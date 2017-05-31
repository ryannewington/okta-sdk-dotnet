// <copyright file="IResource.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace Okta.Sdk.Abstractions
{
    /// <summary>
    /// Marker interface for resources.
    /// </summary>
    public interface IResource
    {
        void Initialize(IChangeTrackingDictionary<string, object> data);

        IDictionary<string, object> GetModifiedData();

        IChangeTrackingDictionary<string, object> GetData();
    }
}
