using Okta.Sdk.Abstractions;
using System;
using System.Linq;
using System.Reflection;

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
