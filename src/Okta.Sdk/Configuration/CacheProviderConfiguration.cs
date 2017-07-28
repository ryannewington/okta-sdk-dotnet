using System;
using Microsoft.Extensions.Caching.Distributed;
using Okta.Sdk.Internal;

namespace Okta.Sdk.Configuration
{
    public sealed class CacheProviderConfiguration : IDeepCloneable<CacheProviderConfiguration>
    {
        public IDistributedCache Provider { get; set; }

        // TODO: timespan?
        public int? DefaultTtl { get; set; } = 300; // Seconds

        public int? DefaultTti { get; set; } = 300; // Seconds

        public CacheProviderConfiguration DeepClone()
        {
            throw new NotImplementedException();
        }
    }
}
