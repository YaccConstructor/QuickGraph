using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using QuickGraph.Unit.Exceptions;

namespace QuickGraph.Unit
{
    public static class Assert
    {
        #region Synching
        private static volatile object syncRoot = new object();
        public static object SyncRoot
        {
            get { return syncRoot; }
        }
        #endregion

        #region Log
        private static IServiceProvider serviceProvider = null;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                lock (syncRoot)
                {
                    return serviceProvider;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    serviceProvider = value;
                }
            }
        }
        public static ILoggerService Logger
        {
            get
            {
                return 
                    ServiceProvider.GetService(typeof(ILoggerService))
                    as ILoggerService;
            }
        }
        #endregion

        #region IsLower, Greater
        public static void IsLowerEqual(object left, object right, IComparer comparer)
        {
            if (comparer.Compare(left, right) > 0)
                Fail("{0} must be lower or equal than {1} (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void IsLowerEqual<T>(T left, T right, IComparer<T> comparer)
        {
            if (comparer.Compare(left, right) > 0)
                Fail("{0} must be lower or equal than {1} (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void IsLowerEqual<T>(IComparable<T> left, T right)
        {
            Assert.IsTrue(left.CompareTo(right)<=0, "{0} must be lower or equal to {1}", left, right);
        }
        public static void IsLowerEqual(IComparable left, IComparable right)
        {
            Assert.IsTrue(left.CompareTo(right) <= 0, "{0} must be lower or equal to {1}", left, right);
        }

        public static void IsLower(object left, object right, IComparer comparer)
        {
            if (comparer.Compare(left, right) >= 0)
                Fail("{0} must be lower than {1} (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void IsLower<T>(T left, T right, IComparer<T> comparer)
        {
            if (comparer.Compare(left, right) >= 0)
                Fail("{0} must be lower than {1} (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void IsLower<T>(IComparable<T> left, T right)
        {
            Assert.IsTrue(left.CompareTo(right) < 0, "{0} must be lower to {1}", left, right);
        }
        public static void IsLower(IComparable left, IComparable right)
        {
            Assert.IsTrue(left.CompareTo(right) < 0, "{0} must be lower to {1}", left, right);
        }

        public static void IsGreaterEqual(object left, object right, IComparer comparer)
        {
            if (comparer.Compare(left, right) < 0)
                Fail("{0} must be greater or equal than {1} (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void IsGreaterEqual<T>(T left, T right, IComparer<T> comparer)
        {
            if (comparer.Compare(left, right) < 0)
                Fail("{0} must be greater or equal than {1} (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void IsGreaterEqual<T>(IComparable<T> left, T right)
        {
            Assert.IsTrue(left.CompareTo(right) >= 0, "{0} must be greater or equal to {1}", left, right);
        }
        public static void IsGreaterEqual(IComparable left, IComparable right)
        {
            Assert.IsTrue(left.CompareTo(right) >= 0, "{0} must be greater or equal to {1}", left, right);
        }

        public static void IsGreater(object left, object right, IComparer comparer)
        {
            if (comparer.Compare(left, right) <= 0)
                Fail("{0} must be greater than {1} (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void IsGreater<T>(T left, T right, IComparer<T> comparer)
        {
            if (comparer.Compare(left, right) <= 0)
                Fail("{0} must be greater than {1} (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void IsGreater<T>(IComparable<T> left, T right)
        {
            Assert.IsTrue(left.CompareTo(right) > 0, "{0} must be greater to {1}", left, right);
        }
        public static void IsGreater(IComparable left, IComparable right)
        {
            Assert.IsTrue(left.CompareTo(right) > 0, "{0} must be greater to {1}", left, right);
        }
        #endregion

        #region AreSame
        public static void AreSame(object actual, object expected)
        {
            Assert.AreEqual(actual, expected);
        }

        public static void AreSame(object actual, object expected, string message)
        {
            Assert.AreEqual(actual, expected, message);
        }

        public static void AreSame(object actual, object expected, string format, params object[] args)
        {
            Assert.AreEqual(actual, expected, format, args);
        }
        #endregion

        #region AreNotEqual
        public static void AreNotEqual<T>(T left, T right, string format, params object[] args)
        {
            string message = String.Format(format,args);
            AreNotEqual<T>(left, right, message);
        }
        public static void AreNotEqual<T>(T left, T right)
        {
            AreNotEqual<T>(left, right, "");
        }
        public static void AreNotEqual<T>(T left, T right, string message)
        {
            if (left == null && right == null)
                throw new AssertionException(String.Format("Objects are both nulls, {0}",message));
            if (left == null && right != null)
                return;
            if (right == null && left != null)
                return;

            if (left.Equals(right))
                throw new AssertionException(
                    String.Format("[{0}]==[{1}], {2}", left, right, message)
                    );

        }
        #endregion

        #region AreEqual
        public static void AreEqual(object left, object right, IComparer comparer)
        {
            if (comparer.Compare(left, right) != 0)
                Fail("[{0}]!=[{1}] (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void AreEqual<T>(T left, T right, IComparer<T> comparer)
        {
            if (comparer.Compare(left, right) != 0)
                Fail("[{0}]!=[{1}] (comparing with {2})",
                    left, right,
                    comparer);
        }

        public static void AreEqual<T>(T left, T right, string message)
        {
            if (left == null && right == null)
                return;
            if (left == null && right != null)
                throw new AssertionException(
                    String.Format("[{0}]!=[null], {1}", left, message)
                    );
            if (right == null && left != null)
                throw new AssertionException(
                    String.Format("[{0}]!=null, {1}", right, message)
                    );


            if (!left.Equals(right))
                throw new AssertionException(
                    String.Format("[{0}]!=[{1}], {2}", left, right, message)
                    );
        }

        public static void AreEqual<T>(T left, T right)
        {
            AreEqual<T>(left, right, 
                "[{0}]!=[{1}]", left, right
                );
        }

        public static void AreEqual<T>(T left, T right, string format, params object[] args)
        {
            string message = String.Format(format, args);
            AreEqual<T>(left, right, message);
        }
#endregion

        #region AreEqualNumeric
        public static void AreEqual(double a, double b, double tolerance)
        {
            if (Math.Abs(a - b) > tolerance)
                Assert.Fail("{0} not equal to {1} (tolerance {2})",
                    a, b, tolerance);
        }
        public static void AreEqual(float a, float b, float tolerance)
        {
            if (Math.Abs(a - b) > tolerance)
                Assert.Fail("{0} not equal to {1} (tolerance {2})",
                    a, b, tolerance);
        }
        #endregion

        #region IsNull, IsNotNull
        public static void IsNull<T>(T o, string message)
        {
            if (o != null)
                throw new AssertionException(
                    String.Format("{0} is not a null reference, {1}", o, message)
                    );
        }

        public static void IsNotNull<T>(T o, string message)
        {
            if (o == null)
                throw new AssertionException(
                    String.Format("{0} is a null reference", o, message)
                    );
        }

        public static void IsNull<T>(T o)
        {
            IsNull(o,"");
        }

        public static void IsNotNull<T>(T o)
        {
            IsNotNull(o, "");
        }


        public static void IsNull<T>(T o, string format, params object[] args)
        {
            string message = String.Format(format, args);
            IsNull(o, message);
        }

        public static void IsNotNull<T>(T o, string format, params object[] args)
        {
            string message = String.Format(format, args);
            IsNotNull(o, message);
        }
#endregion

        #region True, false
        public static void IsTrue(bool value, string message)
        {
            if (!value)
                throw new AssertionException(
                    String.Format("Expected true got false, {0}", message)
                    );
        }

        public static void IsFalse(bool value, string message)
        {
            if (value)
                throw new AssertionException(
                    String.Format("Expected false, got true, {0}", message)
                    );
        }

        public static void IsTrue(bool o)
        {
            IsTrue(o, "");
        }

        public static void IsFalse(bool o)
        {
            IsFalse(o, "");
        }


        public static void IsTrue(bool o, string format, params object[] args)
        {
            string message = String.Format(format, args);
            IsTrue(o, message);
        }

        public static void IsFalse(bool o, string format, params object[] args)
        {
            string message = String.Format(format, args);
            IsFalse(o, message);
        }
        #endregion

        #region Lower, etc...
        public static void LowerEqualThan<T>(T left, T right)
            where T : IComparable<T>
        {
            LowerEqualThan<T>(left, right, "");
        }
        public static void LowerEqualThan<T>(T left, T right, string format, params object[] args)
            where T : IComparable<T>
        {
            string message = String.Format(format, args);
            LowerEqualThan<T>(left, right, message);
        }
        public static void LowerEqualThan<T>(T left, T right, string message)
            where T : IComparable<T>
        {
            if (left.CompareTo(right) > 0)
                throw new AssertionException(
                    String.Format("[{0}]>[{1}], {2}",
                        left, right, message)
                        );
        }
        #endregion

        #region Fail
        public static void Fail()
        {
            throw new AssertionException();
        }

        public static void Fail(string message)
        {
            throw new AssertionException(message);
        }

        public static void Fail(string format, params object[] args)
        {
            string message = String.Format(format, args);
            Fail(message);
        }
        #endregion

        #region Ignore
        public static void Ignore(string message)
        {
            throw new IgnoreException(message);
        }

        public static void Ignore(string format, params object[] parameters)
        {
            Ignore(String.Format(format, parameters));
        }
        #endregion

        #region Warning
        public static void Warning(string message)
        {
            throw new NotImplementedException();
        }

        public static void Warning(string format, params object[] parameters)
        {
            Warning(String.Format(format, parameters));
        }
        #endregion

        #region
        public static void ExpectedException(
            Type expectedException, Delegate test, params object[] args)
        {
            try
            {
                test.DynamicInvoke(args);
                throw new ExceptionNotThrowedException(expectedException);
            }
            catch (Exception ex)
            {
                Exception current = ex;
                // check if current expection is expecetd or ignored
                while (current != null)
                {
                    if (current.GetType() == expectedException)
                        return;
                    current = current.InnerException;
                }
                current = ex;
                if (current is TargetInvocationException)
                    current = current.InnerException;
                throw new ExceptionTypeMistmatchException(expectedException, current);
            }
        }
        #endregion
    }
}
