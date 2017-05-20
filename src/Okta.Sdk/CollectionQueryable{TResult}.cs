using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Okta.Sdk
{
    public class CollectionQueryable<TResult> : IAsyncQueryable<TResult>
    {
        private readonly IDataStore _dataStore;
        private readonly string _uri;
        private readonly KeyValuePair<string, object>[] _initialQueryParameters;

        private readonly Expression _expression;
        private readonly IAsyncQueryProvider _queryProvider;

        public CollectionQueryable(
            IDataStore dataStore,
            string uri,
            IEnumerable<KeyValuePair<string, object>> initialQueryParameters)
        {
            _dataStore = dataStore;
            _uri = uri;
            _initialQueryParameters = initialQueryParameters.ToArray();

            _expression = Expression.Constant(this);
            _queryProvider = new CollectionQueryProvider(_dataStore, _uri, _initialQueryParameters);
        }

        public CollectionQueryable(IAsyncQueryProvider provider, Expression expression)
        {
            if (!(provider is CollectionQueryProvider asCollectionQueryProvider))
                throw new ArgumentException("Asynchronous queries must start from a supported collection.");

            _dataStore = asCollectionQueryProvider.DataStore;
            _uri = asCollectionQueryProvider.Uri;
            _initialQueryParameters = asCollectionQueryProvider.InitialQueryParameters;

            _queryProvider = Ensure.NotNull(provider, nameof(provider));
            _expression = Ensure.NotNull(expression, nameof(expression));
        }

        public Type ElementType => typeof(TResult);

        public Expression Expression => _expression;

        public IAsyncQueryProvider Provider => _queryProvider;

        public IAsyncEnumerator<TResult> GetEnumerator()
            => new CollectionAsyncEnumerator<TResult>(_dataStore, _uri, _initialQueryParameters);

        IAsyncEnumerator<TResult> IAsyncEnumerable<TResult>.GetEnumerator()
            => throw new NotImplementedException();
            //Provider.ExecuteAsync<TResult>(Expression, CancellationToken.None).GetEnumerator();
    }
}
