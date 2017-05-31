using Okta.Sdk.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Okta.Sdk
{
    public partial class OktaClient : IOktaClient
    {
        public OktaClient(IDataStore dataStore)
        {
            DataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        public IDataStore DataStore { get; }

        public UsersClient GetUsersClient => new UsersClient(this);

        public async Task<T> GetAsync<T>(string href, CancellationToken cancellationToken = default(CancellationToken))
            where T : IResource, new()
        {
            var response = await DataStore.GetAsync<T>(href, cancellationToken);
            return response.Payload;
        }

        public async Task<TResponse> PostAsync<TResponse>(
            string href,
            object model,
            CancellationToken cancellationToken = default(CancellationToken))
            where TResponse : IResource, new()
        {
            var response = await DataStore.PostAsync<TResponse>(href, model, cancellationToken);
            return response.Payload;
        }
    }
}
