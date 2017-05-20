using System;
using System.Collections.Generic;
using System.Linq;

namespace Okta.Sdk
{
    public static class Ensure
    {
        public static T NotNull<T>(T obj, string name)
        {
            if (obj == null) throw new ArgumentNullException(name);
            return obj;
        }

        public static string NotNullOrEmpty(string input, string name)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(name);
            return input;
        }

        public static IEnumerable<T> EmptyEnumerable<T>(IEnumerable<T> possiblyEnumerable, string name)
            => possiblyEnumerable ?? Enumerable.Empty<T>();
    }
}
