namespace Okta.Sdk.UnitTests
{
    public class TestNestedResource : TestResource
    {
        public TestNestedResource Nested
        {
            get => GetProperty<TestNestedResource>("nested");
            set => SetResourceProperty("nested", value);
        }
    }
}
