using FluentAssertions;
using Okta.Sdk.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Okta.Sdk.IntegrationTests
{
    public class OktaClientShould
    {
        private readonly IOktaClient _client;

        public OktaClientShould()
        {
            // Initialize the Okta client
            _client = new OktaClient(new ApiClientConfiguration
            {
                OrgUrl = "https://dev-341607.oktapreview.com",
                Token = "asdf"
            });
        }

        [Fact]
        public async Task GetUserByHref()
        {
            var user = await _client.GetAsync<User>("https://dev-341607.oktapreview.com/api/v1/users/00u9o1nikjvOBg5Zo0h7");

            user.Id.Should().Be("00u9o1nikjvOBg5Zo0h7");
        }
    }
}
