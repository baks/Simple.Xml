using System;
using NSubstitute.Core;

namespace Simple.Xml.Structure.UnitTests
{
    public static class WhenCalledExtensions
    {
        private static string savedState = "not changed by test";

        public static void Then<T>(this WhenCalled<T> whenCalled, string newState)
        {
            whenCalled.Do(info => savedState = newState);
        }

        public static void Expect<T>(this WhenCalled<T> whenCalled, string expectedState)
        {
            whenCalled.Do(info =>
            {
                if (!string.Equals(savedState, expectedState, StringComparison.Ordinal))
                {
                    throw new Exception($" Expected {expectedState}, but was {savedState}");
                }
            });
        }
    }
}