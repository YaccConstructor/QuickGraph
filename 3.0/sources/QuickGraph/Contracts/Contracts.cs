#if INLINED_CONTRACTS
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Diagnostics.Contracts
{
    /// <summary>
    /// Methods to express preconditions, postconditions and invariants.
    /// </summary>
    internal static class Contract
    {
        public static event EventHandler<ContractFailedEventArgs> ContractFailed;

#if !SILVERLIGHT
        [Serializable]
#endif
        [DebuggerDisplay("{FailureKind}: {Message}")]
        sealed class ContractException : Exception
        {
            ContractFailureKind failureKind;

            public ContractException(string message, ContractFailureKind failureKind)
                : base(message)
            {
                this.failureKind = failureKind;
            }

            public ContractFailureKind FailureKind
            {
                get { return this.failureKind; }
            }
        }

        /// <summary>
        /// In debug builds, perform a runtime test that a condition is true. 
        /// (shortcut: cca)
        /// </summary>
        /// <param name="condition">the asserted condition</param>
        [Conditional("CONTRACTS_FULL"), Conditional("DEBUG")]
        public static void Assert(bool condition)
        {
            if (!condition)
                Failure(ContractFailureKind.Assert, null);
        }

        /// <summary>
        /// In debug builds, perform a runtime test that a condition is true. 
        /// </summary>
        /// <param name="condition">the asserted condition</param>
        /// <param name="message">custom message</param>
        [Conditional("DEBUG"), Conditional("CONTRACTS_FULL")]
        public static void Assert(bool condition, string message)
        {
            if (!condition)
                Failure(ContractFailureKind.Assert, message);
        }

        /// <summary>
        ///  In debug builds, instructs code analysis tools to assume a condition is true even if it can not be statically proven to always be true. 
        ///  (shortcut: ccam)
        /// </summary>
        /// <param name="condition">the assumed condition</param>
        [Conditional("DEBUG"), Conditional("CONTRACTS_FULL")]
        public static void Assume(bool condition)
        {
            if (!condition)
                Failure(ContractFailureKind.Assume, null);
        }

        /// <summary>
        ///  In debug builds with contracts, instructs code analysis tools to assume a condition is true even if it can not be statically proven to always be true. 
        /// </summary>
        /// <param name="condition">the assumed condition</param>
        /// <param name="message">custom message</param>
        [Conditional("DEBUG"), Conditional("CONTRACTS_FULL")]
        public static void Assume(bool condition, string message)
        {
            if (!condition)
                Failure(ContractFailureKind.Assume, message);
        }

        /// <summary>
        /// Identifies the end of a contract block. Place this method call after legacy parameter validations.
        /// </summary>
        [Conditional("CONTRACTS_FULL")]
        public static void EndContractBlock()
        { }

        /// <summary>
        /// Specifies a public contract such that the conditional expression is true when the enclosing method or property returns normally.
        /// (shortcut: ce)
        /// </summary>
        /// <param name="condition">The conditional expression to test for true. The expression may include OldValue(T) and Result(T).</param>
        [Conditional("CONTRACTS_FULL")]
        public static void Ensures(bool condition)
        {
            // requires rewritter
        }

        /// <summary>
        /// Specifies a public contract such that the conditional expression is true when the enclosing method or property returns normally.
        /// </summary>
        /// <param name="condition">The conditional expression to test for true. The expression may include OldValue(T) and Result(T).</param>
        /// <param name="message">custom logging message</param>
        [Conditional("CONTRACTS_FULL")]
        public static void Ensures(bool condition, string message)
        {
            // requires rewritter
        }

        /// <summary>
        /// Specifies a contract such that if an exception of type TException is thrown then the conditional expression should be true 
        /// when the enclosing method or property terminates abnormally.
        /// </summary>
        /// <param name="condition">The conditional expression to test for true. The expression may include OldValue(T) and Result(T).</param>
        [Conditional("CONTRACTS_FULL")]
        public static void EnsuresOnThrow<TException>(bool condition)
            where TException : Exception
        {
            // requires rewritter
        }

        /// <summary>
        /// Specifies a contract such that if an exception of type TException is thrown then the conditional expression should be true 
        /// when the enclosing method or property terminates abnormally.
        /// </summary>
        /// <param name="condition">The conditional expression to test for true. The expression may include OldValue(T) and Result(T).</param>
        /// <param name="message">custom message</param>
        [Conditional("CONTRACTS_FULL")]
        public static void EnsuresOnThrow<TException>(bool condition, string message)
            where TException : Exception
        {
            // requires rewritter
        }


        /// <summary>
        /// Returns true if predicate returns true for any element in collection
        /// </summary>
        /// <typeparam name="T">the element type of the collection</typeparam>
        /// <param name="collection">the collection</param>
        /// <param name="predicate">the predicate</param>
        /// <returns>true if any element evaluates predicate to true</returns>
        public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            //Contract.Requires<ArgumentNullException>(collection != null);
            //Contract.Requires<ArgumentNullException>(predicate != null);
            // because this assembly is built in custom validation mode (no rewriter in release), we must use legacy-if-then throw
            if (collection == null) throw new ArgumentNullException("collection", "collection != null");
            if (predicate == null) throw new ArgumentNullException("predicate", "predicate != null");
            Contract.EndContractBlock();
            foreach (var local in collection)
            {
                if (predicate(local))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if predicate returns true for any integer starting from inclusiveLowerBound to exclusiveUpperBound - 1.
        /// </summary>
        /// <param name="inclusiveLowerBound">the inclusive lower bound</param>
        /// <param name="exclusiveUpperBound">the exclusive upper bound</param>
        /// <param name="predicate">a method that evaluates an index to a boolean value</param>
        /// <returns>true if predicate returns true for any integer starting from inclusiveLowerBound to exclusiveUpperBound - 1.</returns>
        public static bool Exists(int inclusiveLowerBound, int exclusiveUpperBound, Predicate<int> predicate)
        {
            //Contract.Requires<ArgumentOutOfRangeException>(inclusiveLowerBound <= exclusiveUpperBound);
            //Contract.Requires<ArgumentNullException>(predicate != null);
            if (inclusiveLowerBound > exclusiveUpperBound) throw new ArgumentOutOfRangeException("inclusiveLowerBound <= exclusiveUpperBound");
            if (predicate == null) throw new ArgumentNullException("predicate", "prediate != null");
            Contract.EndContractBlock();

            for (int i = inclusiveLowerBound; i < exclusiveUpperBound; i++)
            {
                if (predicate(i))
                {
                    return true;
                }
            }

            return false;
        }

        [DebuggerNonUserCode]
        private static void Failure(ContractFailureKind failureKind, string message)
        {
            var eh = ContractFailed;
            if (eh != null)
            {
                var args = new ContractFailedEventArgs(failureKind, message, null);
                eh(null, args);
                if (args.Handled)
                    return;
            }

            throw new ContractException(message, failureKind);
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
        {
            if (!condition)
                Failure(ContractFailureKind.Invariant, null);
        }

        [Conditional("CONTRACTS_FULL")]
        public static void Invariant(bool condition, string message)
        {
            if (!condition)
                Failure(ContractFailureKind.Invariant, message);
        }

        public static T OldValue<T>(T value)
        {
            return default(T);
        }

        /// <summary>
        /// Specifies a contract such that a condition must be true before the enclosing method or property is invoked. 
        /// (shortcut: cr, crn, crsn)
        /// </summary>
        /// <param name="condition">The conditional expression to test for true.</param>
        [Conditional("CONTRACTS_FULL")]
        public static void Requires(bool condition)
        {
            if (!condition)
                Failure(ContractFailureKind.Precondition, null);
        }

        /// <summary>
        /// Specifies a contract such that a condition must be true before the enclosing method or property is invoked. 
        /// </summary>
        /// <param name="condition">The conditional expression to test for true.</param>
        /// <param name="message">custom error message</param>
        [Conditional("CONTRACTS_FULL")]
        public static void Requires(bool condition, string message)
        {
            if (!condition)
                Failure(ContractFailureKind.Postcondition, message);
        }

        /// <summary>
        /// Specifies a contract such that a condition must be true when a particular exception type is raised.
        /// (shortcuts: cre, cren, cresn)
        /// </summary>
        /// <param name="condition">The conditional expression to test for true.</param>
        [Conditional("CONTRACTS_FULL")]
        public static void Requires<TException>(bool condition)
            where TException : Exception
        {
            if (!condition)
            {
                if (typeof(TException) == typeof(ArgumentException))
                    throw new ArgumentException();
                else if (typeof(TException) == typeof(ArgumentNullException))
                    throw new ArgumentNullException();
                else if (typeof(TException) == typeof(ArgumentOutOfRangeException))
                    throw new ArgumentOutOfRangeException();
                else
                    throw Activator.CreateInstance<TException>();
            }
        }

        public static T Result<T>()
        {
            // requires rewritter
            return default(T);
        }

        public static T ValueAtReturn<T>(out T value)
        {
            // requires rewritter
            value = default(T);
            return value;
        }
    }

    [AttributeUsage(AttributeTargets.Delegate | AttributeTargets.Parameter | AttributeTargets.Event | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class, AllowMultiple = false, Inherited = true), Conditional("CONTRACTS_PRECONDITIONS"), Conditional("CONTRACTS_FULL")]
    internal sealed class PureAttribute : Attribute
    { }

#if !SILVERLIGHT
    [Serializable]
#endif
    internal enum ContractFailureKind
    {
        Precondition,
        Postcondition,
        Invariant,
        Assert,
        Assume
    }

    [DebuggerDisplay("{FailureKind}: {Condition}")]
#if !SILVERLIGHT
    [Serializable]
#endif
    internal sealed class ContractFailedEventArgs
        : EventArgs
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

    [Conditional("CONTRACTS_FULL"), AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    internal sealed class ContractInvariantMethodAttribute
        : Attribute
    {
    }

    [Conditional("CONTRACTS_FULL"), AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal sealed class ContractClassForAttribute
        : Attribute
    {
        public Type TypeContractsAreFor { get; private set; }
        public ContractClassForAttribute(Type type)
        {
            if (type == null) throw new ArgumentNullException("type", "type != null");
            Contract.EndContractBlock();

            this.TypeContractsAreFor = type;
        }
    }

    [Conditional("CONTRACTS_FULL"), AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal sealed class ContractClassAttribute
        : Attribute
    {
        public Type TypeContainingContracts { get; private set; }
        public ContractClassAttribute(Type type)
        {
            if (type == null) throw new ArgumentNullException("type", "type != null");
            Contract.EndContractBlock();

            this.TypeContainingContracts = type;
        }
    }

    [AttributeUsage(AttributeTargets.Field), Conditional("CONTRACTS_FULL")]
    internal sealed class ContractPublicPropertyNameAttribute : Attribute
    {
        // Methods
        public ContractPublicPropertyNameAttribute(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("!String.IsNullOrEmpty(name)");
            Contract.EndContractBlock();

            this.Name = name;
        }

        // Properties
        public string Name { get; private set; }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    internal sealed class ContractRuntimeIgnoredAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    internal sealed class ContractReferenceAssemblyAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    internal sealed class ContractVerificationAttribute : Attribute
    {
        public ContractVerificationAttribute(bool value) { this.Value = value; }
        public bool Value { get; private set; }
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