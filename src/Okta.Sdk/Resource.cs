// <copyright file="Resource.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;

namespace Okta.Sdk
{
    public class Resource
    {
        private readonly ResourceFactory _resourceFactory;
        private readonly ResourceDictionaryType _dictionaryType;
        private IDictionary<string, object> _data;

        public Resource()
            : this(ResourceDictionaryType.Default)
        {
        }

        public Resource(ResourceDictionaryType dictionaryType)
        {
            _resourceFactory = new ResourceFactory();
            _dictionaryType = dictionaryType;
            Initialize(null);
        }

        public void Initialize(IDictionary<string, object> data)
        {
            _data = data ?? _resourceFactory.NewDictionary(_dictionaryType);
        }

        public IDictionary<string, object> GetModifiedData()
        {
            switch (_data)
            {
                case DefaultChangeTrackingDictionary changeTrackingDictionary:
                    return (IDictionary<string, object>)changeTrackingDictionary.Difference;
                default:
                    return _data;
            }
        }

        public void SetProperty(string key, object value)
            => _data[key] = value;

        public void SetResourceProperty<T>(string key, T value)
            where T : Resource
            => SetProperty(key, value?._data);

        public object GetProperty(string key)
        {
            _data.TryGetValue(key, out var value);
            return value;
        }

        public string GetStringProperty(string key)
            => GetProperty(key)?.ToString();

        public bool? GetBooleanProperty(string key)
        {
            var raw = GetStringProperty(key);
            if (raw == null)
            {
                return null;
            }

            return bool.Parse(raw);
        }

        public int? GetIntProperty(string key)
        {
            var raw = GetStringProperty(key);
            if (raw == null)
            {
                return null;
            }

            return int.Parse(raw);
        }

        public long? GetLongProperty(string key)
        {
            var raw = GetStringProperty(key);
            if (raw == null)
            {
                return null;
            }

            return long.Parse(raw);
        }

        public DateTimeOffset? GetDateTimeProperty(string key)
        {
            var raw = GetStringProperty(key);
            if (raw == null)
            {
                return null;
            }

            return DateTimeOffset.Parse(raw);
        }

        public IList<T> GetListProperty<T>(string key)
        {
            throw new NotImplementedException();
        }

        public T GetProperty<T>(string key)
            where T : Resource, new()
        {
            var nestedData = GetProperty(key) as IDictionary<string, object>;
            return _resourceFactory.Create<T>(nestedData);
        }
    }
}
