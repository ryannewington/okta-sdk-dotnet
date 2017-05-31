using FluentAssertions;
using NSubstitute;
using Okta.Sdk.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Okta.Sdk.UnitTests
{
    public class DefaultDataStoreShould
    {
        [Fact]
        public async Task HandleNullExecutorResponseDuringGet()
        {
            // If the RequestExecutor returns a null HttpResponse, throw an informative exception.

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            var dataStore = new DefaultDataStore(mockRequestExecutor, new DefaultSerializer());

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => dataStore.GetAsync<TestResource>("https://foo.dev", CancellationToken.None));
        }

        [Fact]
        public async Task HandleNullPayloadDuringGet()
        {
            // If the API returns a null or empty payload, it shouldn't cause an error.

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .GetAsync("https://foo.dev", CancellationToken.None)
                .Returns(new HttpResponse<string>() { StatusCode = 200, Payload = null });
            var dataStore = new DefaultDataStore(mockRequestExecutor, new DefaultSerializer());

            var response = await dataStore.GetAsync<TestResource>("https://foo.dev", CancellationToken.None);
            response.StatusCode.Should().Be(200);

            response.Payload.Should().NotBeNull();
            response.Payload.Foo.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task HandleEmptyPayloadDuringGet()
        {
            // If the API returns a null or empty payload, it shouldn't cause an error.

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .GetAsync("https://foo.dev", CancellationToken.None)
                .Returns(new HttpResponse<string>() { StatusCode = 200, Payload = string.Empty });
            var dataStore = new DefaultDataStore(mockRequestExecutor, new DefaultSerializer());

            var response = await dataStore.GetAsync<TestResource>("https://foo.dev", CancellationToken.None);
            response.StatusCode.Should().Be(200);

            response.Payload.Should().NotBeNull();
            response.Payload.Foo.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task HandleNullExecutorResponseDuringGetArray()
        {
            // If the RequestExecutor returns a null HttpResponse, throw an informative exception.

            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            var dataStore = new DefaultDataStore(mockRequestExecutor, new DefaultSerializer());

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => dataStore.GetArrayAsync<TestResource>("https://foo.dev", CancellationToken.None));
        }

        [Fact]
        public async Task DelegateGetToRequestExecutor()
        {
            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .GetAsync("https://foo.dev", CancellationToken.None)
                .Returns(new HttpResponse<string>() { StatusCode = 200 });
            var dataStore = new DefaultDataStore(mockRequestExecutor, new DefaultSerializer());

            await dataStore.GetAsync<TestResource>("https://foo.dev", CancellationToken.None);

            await mockRequestExecutor.Received().GetAsync("https://foo.dev", CancellationToken.None);
        }

        [Fact]
        public async Task DelegatePostToRequestExecutor()
        {
            var mockRequestExecutor = Substitute.For<IRequestExecutor>();
            mockRequestExecutor
                .PostAsync("https://foo.dev", "{}", CancellationToken.None)
                .Returns(new HttpResponse<string>() { StatusCode = 200 });
            var dataStore = new DefaultDataStore(mockRequestExecutor, new DefaultSerializer());

            await dataStore.PostAsync<TestResource>("https://foo.dev", new { }, CancellationToken.None);

            await mockRequestExecutor.Received().PostAsync("https://foo.dev", "{}", CancellationToken.None);
        }
    }
}
