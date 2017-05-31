using Okta.Sdk.Abstractions;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Okta.Sdk.UnitTests
{
    public class ResourceCreator<T>
        where T : IResource, new()
    {
        private readonly IChangeTrackingDictionary<string, object> _data
            = new DefaultChangeTrackingDictionary(keyComparer: StringComparer.OrdinalIgnoreCase);

        public ResourceCreator<T> With(Expression<Func<T, object>> propertySelector, object value)
        {
            var propertyInfo = GetPropertyName(propertySelector);

            _data.Add(propertyInfo.Name, value);

            return this;
        }

        public ResourceCreator<T> With(params (Expression<Func<T, object>> propertySelector, object value)[] setters)
        {
            foreach (var (selector, value) in setters)
            {
                With(selector, value);
            }

            return this;
        }

        public static implicit operator T(ResourceCreator<T> creator)
        {
            var factory = new ResourceFactory();
            return factory.Create<T>(creator._data);
        }

        private static PropertyInfo GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var body = propertyLambda.Body;

            if (!(body is MemberExpression member))
                throw new ArgumentException($"Expression '{propertyLambda}' does not refer to a property.");

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

            return propertyInfo;
        }
    }
}
