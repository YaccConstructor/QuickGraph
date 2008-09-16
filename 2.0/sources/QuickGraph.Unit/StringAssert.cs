using System;
using System.Text;

namespace QuickGraph.Unit
{
    public static class StringAssert
    {
        public static void AreEqual(string expected, string value, StringComparison comparaison)
        {
            Assert.IsTrue(expected.Equals(value, comparaison),
                "[{0}]!=[{1}] using comparaison {2}",
                expected, value, comparaison);
        }

        public static void AreNotEqual(string expected, string value, StringComparison comparaison)
        {
            Assert.IsFalse(expected.Equals(value, comparaison),
                                "[{0}]==[{1}] using comparaison {2}",
                expected, value, comparaison);
        }

        public static void Contains(string value, string match)
        {
            Assert.IsTrue(value.Contains(match),
                "[{0}] does not contain [{1}]", value, match);
        }

        public static void EndsWith(string value, string match)
        {
            EndsWith(value, match, StringComparison.CurrentCulture);
        }

        public static void EndsWith(string value, string match, StringComparison stringComparaison)
        {
            Assert.IsTrue(value.EndsWith(match, stringComparaison),
                "[{0}] does not end with [{1}]", value, match);
        }

        public static void StartsWith(string value, string match)
        {
            StartsWith(value, match, StringComparison.CurrentCulture);
        }

        public static void StartsWith(string value, string match, StringComparison stringComparaison)
        {
            Assert.IsTrue(value.StartsWith(match,stringComparaison),
                "[{0}] does not end with [{1}]", value, match);
        }

        public static void IsNullOrEmpty(string value)
        {
            Assert.IsTrue(String.IsNullOrEmpty(value),
                "[[{0}]] is not null or empty", value);
        }

        public static void IsNotNullOrEmpty(string value)
        {
            Assert.IsFalse(String.IsNullOrEmpty(value),
                "[[{0}]] is not null or empty", value);
        }
    }
}
