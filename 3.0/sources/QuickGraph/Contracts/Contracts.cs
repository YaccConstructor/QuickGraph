#if INLINED_CONTRACTS
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Diagnostics.Contracts
{
    internal static class Contract
    {
        // Events
        public static event EventHandler<ContractFailedEventArgs> ContractFailed;

        [Conditional("CONTRACTS_FULL"), Conditional("DEBUG")]
        public static void Assert(bool condition)
        {}

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
        public static void Failure(ContractFailureKind failureKind, string message)
        {
            var eh = ContractFailed;
            if (eh != null)
            {
                var args = new ContractFailedEventArgs(failureKind, message, null);
                eh(null, args);
                if (args.Handled)
                    return;
            }

            throw new ArgumentException(message);
        }

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
                Failure(ContractFailureKind.Precondition, null);
        }

        [DebuggerNonUserCode]
        public static void RequiresAlways(bool condition, string message)
        {
            if (!condition)
                Failure(ContractFailureKind.Precondition, message);
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

    [AttributeUsage(AttributeTargets.Delegate | AttributeTargets.Parameter | AttributeTargets.Event | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class, AllowMultiple = false, Inherited = true), Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
    internal sealed class PureAttribute : Attribute
    { }

    internal enum ContractFailureKind
    {
        Precondition,
        Postcondition,
        Invariant,
        Assert,
        Assume
    }

    internal sealed class ContractFailedEventArgs : EventArgs
    {
        // Fields
        private readonly string _condition;
        private readonly string _debugMessage;
        private readonly ContractFailureKind _failureKind;

        // Methods
        public ContractFailedEventArgs(ContractFailureKind failureKind, string debugMessage, string condition)
        {
            this._failureKind = failureKind;
            this._debugMessage = debugMessage;
            this._condition = condition;
        }

        // Properties
        public string Condition
        {
            get
            {
                return this._condition;
            }
        }

        public string DebugMessage
        {
            get
            {
                return this._debugMessage;
            }
        }

        public ContractFailureKind FailureKind
        {
            get
            {
                return this._failureKind;
            }
        }

        public bool Handled { get; set; }
    }

    [Conditional("CONTRACTS_FULL"), Conditional("CONTRACTS_PRECONDITIONS"), AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal sealed class ContractInvariantMethodAttribute : Attribute
    {
    }

    [Conditional("CONTRACTS_FULL"), AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal sealed class ContractClassForAttribute : Attribute
    {
        public Type TypeContractsAreFor { get; private set; }
        public ContractClassForAttribute(Type type)
        {
            this.TypeContractsAreFor = type;
        }
    }

    [Conditional("CONTRACTS_FULL"), AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal sealed class ContractClassAttribute : Attribute
    {
        public Type TypeContainingContracts { get; private set; } 
        public ContractClassAttribute(Type type) 
        {
            this.TypeContainingContracts = type;
        }
    }
}

namespace QuickGraph.Algorithms.Contracts
{
    /// <summary>
    /// Ensures that System.Diagnostics.Contracts namespace exists 
    /// </summary>
    class DummyAlgorithmContract
    { }
}

#endif