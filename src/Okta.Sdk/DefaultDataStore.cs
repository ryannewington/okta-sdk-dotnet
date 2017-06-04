// <copyright file="DefaultDataStore.cs" company="Okta, Inc">
// Copyright (c) 2014-2017 Okta, Inc. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Okta.Sdk.Abstractions;

namespace Okta.Sdk
{
    public sealed class DefaultDataStore : IDataStore
    {
        private readonly IRequestExecutor _requestExecutor;
        private readonly ISerializer _serializer;
        private readonly ResourceFactory _resourceFactory;

        public DefaultDataStore(
            IRequestExecutor requestExecutor,
            ISerializer serializer)
        {
            _requestExecutor = requestExecutor ?? throw new ArgumentNullException(nameof(requestExecutor));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _resourceFactory = new ResourceFactory();
        }

        public IRequestExecutor RequestExecutor => _requestExecutor;

        public ISerializer Serializer => _serializer;

        private HttpResponse<T> HandleResponse<T>(HttpResponse<string> response)
            where T : Resource, new()
        {
            if (response == null)
            {
                throw new InvalidOperationException("The response from the RequestExecutor was null.");
            }

            var dictionary = _serializer.Deserialize(response.Payload ?? string.Empty);
            var changeTrackingDictionary = new DefaultChangeTrackingDictionary(dictionary, StringComparer.OrdinalIgnoreCase);

            if (response.StatusCode != 200)
            {
                throw new OktaApiException(response.StatusCode, _resourceFactory.Create<Resource>(changeTrackingDictionary));
            }

            var resource = _resourceFactory.Create<T>(changeTrackingDictionary);

            return new HttpResponse<T>
            {
                StatusCode = response.StatusCode,
                Headers = response.Headers,
                Payload = resource,
            };
        }

        public async Task<HttpResponse<T>> GetAsync<T>(string href, CancellationToken cancellationToken)
            where T : Resource, new()
        {
            // todo optional query string parameters

            var response = await _requestExecutor.GetAsync(href, cancellationToken).ConfigureAwait(false);
            return HandleResponse<T>(response);
        }

        public async Task<HttpResponse<IEnumerable<T>>> GetArrayAsync<T>(string href, CancellationToken cancellationToken)
            where T : Resource, new()
        {
            // todo optional query string parameters

            var response = await _requestExecutor.GetAsync(href, cancellationToken).ConfigureAwait(false);
            if (response == null)
            {
                throw new InvalidOperationException("The response from the RequestExecutor was null.");
            }

            var resources = _serializer
                .DeserializeArray(response.Payload ?? string.Empty)
                .Select(x => _resourceFactory.Create<T>(new DefaultChangeTrackingDictionary(x, StringComparer.OrdinalIgnoreCase)));

            return new HttpResponse<IEnumerable<T>>
            {
                StatusCode = response.StatusCode,
                Headers = response.Headers,
                Payload = resources,
            };
        }

        public async Task<HttpResponse<TResponse>> PostAsync<TResponse>(string href, object postData, CancellationToken cancellationToken)
            where TResponse : Resource, new()
        {
            var body = _serializer.Serialize(postData);
            // TODO apply query string parameters

            var response = await _requestExecutor.PostAsync(href, body, cancellationToken).ConfigureAwait(false);
            return HandleResponse<TResponse>(response);
        }

        public async Task<HttpResponse<TResponse>> PutAsync<TResponse>(string href, object postData, CancellationToken cancellationToken) where TResponse : Resource, new()
        {
            var body = _serializer.Serialize(postData);
            // TODO apply query string parameters

            var response = await _requestExecutor.PutAsync(href, body, cancellationToken).ConfigureAwait(false);
            return HandleResponse<TResponse>(response);
        }

        public async Task<HttpResponse> DeleteAsync(string href, CancellationToken cancellationToken)
        {
            var response = await _requestExecutor.DeleteAsync(href, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
