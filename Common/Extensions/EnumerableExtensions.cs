using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static IDictionary<TKey, TElement> ToDictionaryDistinct<T, TKey, TElement>(this IEnumerable<T> items, Func<T, TKey> keySelector, Func<T, TElement> elementSelector)
        {
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return items.Select(i => keySelector.Invoke(i))
                        .Distinct()
                        .Select(key =>
                        {
                            var entry = items.First(i => keySelector.Invoke(i).Equals(key));
                            return new KeyValuePair<TKey, TElement>(key, elementSelector(entry));
                        })
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
