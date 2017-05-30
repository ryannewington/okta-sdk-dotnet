using Okta.Sdk.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Okta.Sdk
{
    public sealed class DefaultChangeTrackingList<T> : IChangeTrackingList<T>
    {
        private readonly IChangeTrackable _parent;
        private readonly string _parentKey;

        private readonly IReadOnlyList<T> _initialData;
        private readonly IEqualityComparer<T> _comparer;

        private IList<T> _data;
        private bool _dirty;

        public DefaultChangeTrackingList(
            IEnumerable<T> initialData = null)
            : this(null, null, initialData)
        {
        }

        public DefaultChangeTrackingList(
            IChangeTrackable parent,
            string parentKey,
            IEnumerable<T> initialData = null,
            IEqualityComparer<T> comparer = null)
        {
            if (parent != null)
            {
                if (string.IsNullOrEmpty(parentKey)) throw new ArgumentNullException(nameof(parent), $"Both {nameof(parent)} and {nameof(parentKey)} must be specified.");

                _parent = parent;
                _parentKey = parentKey;
            }

            _initialData = initialData == null
                ? new List<T>()
                : DeepCopy(initialData);

            Reset();
        }
    }
}
