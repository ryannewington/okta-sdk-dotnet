using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Okta.Sdk
{
    public sealed class CollectionQueryProvider : IAsyncQueryProvider
    {
        public CollectionQueryProvider(
            IDataStore dataStore, 
            string uri,
            KeyValuePair<string, object>[] initialQueryParameters)
        {
            DataStore = dataStore;
            Uri = uri;
            InitialQueryParameters = initialQueryParameters;
        }

        public IDataStore DataStore { get; set; }

        public string Uri { get; }

        public KeyValuePair<string, object>[] InitialQueryParameters { get; }

        public IAsyncQueryable<TElement> CreateQuery<TElement>(Expression expression)
            => new CollectionQueryable<TElement>(this, expression);

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken token)
        {
            // TODO: Compile the expression tree

            // Create and use an AsyncEnumerator to return the result

            throw new NotImplementedException();
        }
    }
}
