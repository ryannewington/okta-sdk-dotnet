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
