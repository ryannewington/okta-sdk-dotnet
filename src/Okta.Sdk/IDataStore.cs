using Okta.Sdk.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okta.Sdk
{
    public interface IDataStore
    {
        IRequestExecutor RequestExecutor { get; }

        ISerializer Serializer { get; }

        Task<HttpResponse<T>> GetAsync<T>(string href, CancellationToken cancellationToken)
            where T : IResource, new();

        Task<HttpResponse<IEnumerable<T>>> GetArrayAsync<T>(string href, CancellationToken cancellationToken)
            where T : IResource, new();

        Task<HttpResponse<TResponse>> PostAsync<TResponse>(string href, object postData, CancellationToken cancellationToken)
            where TResponse : IResource, new();
    }
}
