using System;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Collections;

namespace OpenApiBuilder.Tests.TestUtilities
{
    public static class AssertionExtensions
    {
        public static void HaveKey<TKey, TValue>(this GenericDictionaryAssertions<TKey, TValue> dictAssertions,
                                                 TKey key, 
                                                 string because="")
        {
            dictAssertions.Subject.ContainsKey(key).Should().BeTrue(because);
        }

        public static void HaveKeys<TKey, TValue>(this GenericDictionaryAssertions<TKey, TValue> dictAssertions,
            params TKey[] keys)
        {
            foreach (var key in keys)
                dictAssertions.Subject.ContainsKey(key).Should().BeTrue($"Should have key {key}");
        }

        public static void Have<T>(
            this GenericCollectionAssertions<T> listAssertions,
            Func<T, bool> finder,
            int expectedCount = 1,
            string because = "")
        {
            listAssertions.Subject.Count(finder).Should().Be(expectedCount, because);
        }

        public static void Have<T>(this GenericCollectionAssertions<T> listAssertions,
                                   Func<T, bool> finder,
                                   string because="")
            => listAssertions.Have<T>(finder, 1, because);
    }
}
