#if !CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Diagnostics.Contracts
{
    public static class Contract
    {
        // Events
        public static event EventHandler<ContractFailedEventArgs> ContractFailed;

        [Conditional("CONTRACTS_FULL"), Conditional("DEBUG")]
        public static void Assert(bool condition)
        { }

        [Conditional("DEBUG"), Conditional("CONTRACTS_FULL")]
        public static void Assert(bool condition, string message)
        { }

        [Conditional("DEBUG"), Conditional("CONTRACTS_FULL")]
        public static void Assume(bool condition)
        { }

        [Conditional("DEBUG"), Conditional("CONTRACTS_FULL")]
        public static void Assume(bool condition, string message)
        { }

        [Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
        public static void EndContractBlock()
        { }

        [Conditional("CONTRACTS_FULL")]
        public static void Ensures(bool condition)
        { }

        [Conditional("CONTRACTS_FULL")]
        public static void Ensures(bool condition, string message)
        { }

        [Conditional("CONTRACTS_FULL")]
        public static void EnsuresOnThrow<TException>(bool condition)
            where TException : Exception
        { }

        [Conditional("CONTRACTS_FULL")]
        public static void EnsuresOnThrow<TException>(bool condition, string message)
            where TException : Exception
        { }

        public static bool Exists<T>(Predicate<T> predicate)
        {
            return false;
        }

        public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            return false;
        }

        public static bool Exists(int inclusiveLowerBound, int exclusiveUpperBound, Predicate<int> predicate)
        {
            return false;
        }

        [DebuggerNonUserCode]
        public static void Failure(ContractFailureKind failureKind, string userProvidedMessage, string condition)
        { }

        public static bool ForAll<T>(Predicate<T> predicate)
        {
            return false;
        }

        public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            return false;
        }

        public static bool ForAll(int inclusiveLowerBound, int exclusiveUpperBound, Predicate<int> predicate)
        {
            return false;
        }

        [Conditional("CONTRACTS_FULL")]
        public static void Invariant(bool condition)
        { }

        [Conditional("CONTRACTS_FULL")]
        public static void Invariant(bool condition, string message)
        { }

        public static T OldValue<T>(T value)
        {
            return default(T);
        }

        [Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
        public static void Requires(bool condition)
        {
        }

        [Conditional("CONTRACTS_FULL"), Conditional("CONTRACTS_PRECONDITIONS")]
        public static void Requires(bool condition, string message)
        {
        }

        [DebuggerNonUserCode]
        public static void RequiresAlways(bool condition)
        {
            if (!condition)
                throw new ArgumentException();
        }

        [DebuggerNonUserCode]
        public static void RequiresAlways(bool condition, string message)
        {
            if (!condition)
                throw new ArgumentException(message);
        }

        public static T Result<T>()
        {
            return default(T);
        }

        public static T ValueAtReturn<T>(out T value)
        {
            value = default(T);
            return value;
        }
    }
}
#endif