// <copyright file="ResourceFactory.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Linq;
using System.Reflection;
using Okta.Sdk.Abstractions;

namespace Okta.Sdk
{
    public sealed class ResourceFactory
    {
        public DefaultChangeTrackingDictionary NewDictionary()
            => new DefaultChangeTrackingDictionary(keyComparer: StringComparer.OrdinalIgnoreCase);

        public T Create<T>(IChangeTrackingDictionary<string, object> data)
            where T : IResource, new()
        {
            var model = new T();
            model.Initialize(data);
            return model;
        }
    }
}
