using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;

namespace Okta.Sdk
{
    public class Resource : IResource
    {
        private readonly ResourceFactory _resourceFactory;
        private IChangeTrackingDictionary<string, object> _data;

        public Resource()
        {
            _resourceFactory = new ResourceFactory();
            Initialize();
        }

        public void Initialize(IChangeTrackingDictionary<string, object> data = null)
        {
            _data = data ?? _resourceFactory.NewDictionary();
        }

        public IChangeTrackingDictionary<string, object> GetData() => _data;

        public IDictionary<string, object> GetModifiedData() => (IDictionary<string, object>)_data.Difference;

        public void SetProperty(string key, object value)
            => _data[key] = value;

        public void SetResourceProperty<T>(string key, T value)
            where T : IResource
            => SetProperty(key, value?.GetData());

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
            if (raw == null) return null;
            return bool.Parse(raw);
        }

        public int? GetIntProperty(string key)
        {
            var raw = GetStringProperty(key);
            if (raw == null) return null;
            return int.Parse(raw);
        }

        public long? GetLongProperty(string key)
        {
            var raw = GetStringProperty(key);
            if (raw == null) return null;
            return long.Parse(raw);
        }

        public DateTimeOffset? GetDateTimeProperty(string key)
        {
            var raw = GetStringProperty(key);
            if (raw == null) return null;
            return DateTimeOffset.Parse(raw);
        }

        public IList<T> GetListProperty<T>(string key)
        {
            throw new NotImplementedException();
        }

        public T GetProperty<T>(string key)
            where T : IResource, new()
        {
            var nestedData = GetProperty(key) as IChangeTrackingDictionary<string, object>;
            return _resourceFactory.Create<T>(nestedData);
        }
    }
}
