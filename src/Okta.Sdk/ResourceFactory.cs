// <copyright file="ResourceFactory.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;

namespace Okta.Sdk
{
    public sealed class ResourceFactory
    {
        public IDictionary<string, object> NewDictionary(ResourceDictionaryType type)
        {
            switch (type)
            {
                case ResourceDictionaryType.Default: return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                case ResourceDictionaryType.ChangeTracking: return new DefaultChangeTrackingDictionary(keyComparer: StringComparer.OrdinalIgnoreCase);
            }

            throw new ArgumentException($"Unknown resource dictionary type {type}");
        }

        public T Create<T>(IDictionary<string, object> data)
            where T : Resource, new()
        {
            var resource = new T();
            resource.Initialize(data);
            return resource;
        }
    }
}
