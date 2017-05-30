using System.Collections.Generic;

namespace Okta.Sdk.Abstractions
{
    public interface IChangeTrackingList<T> : IList<T>, IChangeTrackable
    {
    }
}
