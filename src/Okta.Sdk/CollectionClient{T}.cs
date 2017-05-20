using Okta.Sdk.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Okta.Sdk
{
    public sealed class CollectionClient<T> : CollectionQueryable<T>
    {
        public CollectionClient(
            IDataStore dataStore,
            string uri,
            IEnumerable<KeyValuePair<string, object>> queryParameters)
            : base(Ensure.NotNull(dataStore, nameof(dataStore)),
                   Ensure.NotNullOrEmpty(uri, nameof(uri)),
                   Ensure.EmptyEnumerable(queryParameters, nameof(queryParameters)))
        {
        }
    }
}
