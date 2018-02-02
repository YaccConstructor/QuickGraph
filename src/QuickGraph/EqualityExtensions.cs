using System;
using System.Collections.Generic;

namespace QuickGraph
{
    /// <summary>
    /// Deep equality methods for collections (arrays, dictionaries) implemented as extension methods.
    /// </summary>
    /// The default equality method, Equals, usually gives reference equality for arrays, dictionaries 
    /// and other collections.  What if you have two distinct arrays and you want to check whether they
    /// have equal lengths and contain identical elements?  That's when you use Equals1.
    /// What if you have arrays of arrays?  Then use Equals2.  And so on with Equals3, etc.
    public static class EqualityExtensions
    {
        #region For IList<T>

        /// <summary>
        /// Element-by-element array equality using a given equality comparer for elements.
        /// Two arrays are equal iff they are both null,
        /// or are actually the same reference,
        /// or they have equal length and elements are equal for each index. 
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="lhs">First array</param>
        /// <param name="rhs">Second array</param>
        /// <param name="elementEquality">Equality comparer for type T</param>
        /// <returns>Whether the two arrays are equal</returns>
        public static bool Equals1<T>(this IList<T> lhs, IList<T> rhs, IEqualityComparer<T> elementEquality)
        {
            if (ReferenceEquals(lhs, null))
            {
                return ReferenceEquals(rhs, null);
            }

            if (ReferenceEquals(rhs, null))
            {
                return ReferenceEquals(lhs, null);
            }

            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (lhs.Count != rhs.Count)
            {
                return false;
            }

            for (int i = 0; i < lhs.Count; i++)
            {
                if (!elementEquality.Equals(lhs[i], rhs[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Element-by-element array equality using EqualityComparer(T).Default to equate elements.
        /// Two arrays are equal iff they are both null,
        /// or are actually the same reference,
        /// or they have equal length and elements are equal for each index. 
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="lhs">First array</param>
        /// <param name="rhs">Second array</param>
        /// <returns>Whether the two arrays are equal</returns>
        public static bool Equals1<T>(this IList<T> lhs, IList<T> rhs)
        {
            return Equals1(lhs, rhs, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Specialization of ArrayEquals(T) augmented with tolerance value
        /// to be used when equating two float values.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Equals1(this IList<float> lhs, IList<float> rhs, float tolerance)
        {
            return Equals1(lhs, rhs, new FloatEqualityComparer(tolerance));
        }

        /// <summary>
        /// Specialization of ArrayEquals{T} augmented with tolerance value
        /// to be used when equating two double values.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Equals1(this IList<double> lhs, IList<double> rhs, double tolerance)
        {
            return Equals1(lhs, rhs, new DoubleEqualityComparer(tolerance));
        }

        #endregion

        #region For Array

        /// <summary>
        /// Element-by-element array equality using a given equality comparer for elements.
        /// Two arrays are equal iff they are both null,
        /// or are actually the same reference,
        /// or they have equal length and elements are equal for each index. 
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="lhs">First array</param>
        /// <param name="rhs">Second array</param>
        /// <param name="elementEquality">Equality comparer for type T</param>
        /// <returns>Whether the two arrays are equal</returns>
        public static bool Equals1<T>(this T[] lhs, T[] rhs, IEqualityComparer<T> elementEquality)
        {
            if (ReferenceEquals(lhs, null))
            {
                return ReferenceEquals(rhs, null);
            }

            if (ReferenceEquals(rhs, null))
            {
                return ReferenceEquals(lhs, null);
            }

            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (lhs.Length != rhs.Length)
            {
                return false;
            }

            for (int i = 0; i < lhs.Length; i++)
            {
                if (!elementEquality.Equals(lhs[i], rhs[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Element-by-element array equality using EqualityComparer(T).Default to equate elements.
        /// Two arrays are equal iff they are both null,
        /// or are actually the same reference,
        /// or they have equal length and elements are equal for each index. 
        /// </summary>
        /// <typeparam name="T">Element type</typeparam>
        /// <param name="lhs">First array</param>
        /// <param name="rhs">Second array</param>
        /// <returns>Whether the two arrays are equal</returns>
        public static bool Equals1<T>(this T[] lhs, T[] rhs)
        {
            return Equals1(lhs, rhs, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Specialization of ArrayEquals(T) augmented with tolerance value
        /// to be used when equating two float values.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Equals1(this float[] lhs, float[] rhs, float tolerance)
        {
            return Equals1(lhs, rhs, new FloatEqualityComparer(tolerance));
        }

        /// <summary>
        /// Specialization of ArrayEquals{T} augmented with tolerance value
        /// to be used when equating two double values.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Equals1(this double[] lhs, double[] rhs, double tolerance)
        {
            return Equals1(lhs, rhs, new DoubleEqualityComparer(tolerance));
        }

        /// <summary>
        /// 2-level deep equality for array of arrays of T using EqualityComparer(T).Default as element equality.
        /// This has '2' in its name, because one may want to do partially deep equality on multi-dimensional array type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Is it really an error?")]
        public static bool Equals2<T>(this T[][] lhs, T[][] rhs)
        {
            return Equals2(lhs, rhs, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// 2-level deep equality for array of arrays of T using a given element equality comparer.
        /// This has '2' in its name, because one may want to do partially deep equality on multi-dimensional array type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <param name="elementEquality"></param>
        /// <returns></returns>
        public static bool Equals2<T>(this T[][] lhs, T[][] rhs, IEqualityComparer<T> elementEquality)
        {
            return Equals1(lhs, rhs, new ArrayEqualityComparer<T>(elementEquality));
        }

        /// <summary>
        /// 3-level deep equality for array of arrays of arrays of T using EqualityComparer(T).Default as element equality.
        /// This has '3' in its name, because one may want to do partially deep equality on multi-dimensional array type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Is it really an error?")]
        public static bool Equals3<T>(this T[][][] lhs, T[][][] rhs)
        {
            return Equals3(lhs, rhs, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// 3-level deep equality for array of arrays of arrays of T using a given element equality comparer.
        /// This has '3' in its name, because one may want to do partially deep equality on multi-dimensional array type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <param name="elementEquality"></param>
        /// <returns></returns>
        public static bool Equals3<T>(this T[][][] lhs, T[][][] rhs, IEqualityComparer<T> elementEquality)
        {
            return Equals2(lhs, rhs, new ArrayEqualityComparer<T>(elementEquality));
        }
        
        #endregion

        #region For IDictionary

        /// <summary>
        /// Element-by-element dictionary equality using the default equality comparer for values.
        /// Two dictionaries are equal iff they are both null,
        /// or are actually the same reference,
        /// or they have identical sets of keys and elements are equal for each key. 
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="lhs">First dictionary</param>
        /// <param name="rhs">Second dictionary</param>
        /// <returns>Whether the two dictionaries are equal.</returns>
        public static bool Equals1<TKey, TValue>(this IDictionary<TKey, TValue> lhs, IDictionary<TKey, TValue> rhs)
        {
            return Equals1(lhs, rhs, EqualityComparer<TValue>.Default);
        }

        /// <summary>
        /// Element-by-element dictionary equality using a given equality comparer for values.
        /// Two dictionaries are equal iff they are both null,
        /// or are actually the same reference,
        /// or they have identical sets of keys and elements are equal for each key. 
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="lhs">First dictionary</param>
        /// <param name="rhs">Second dictionary</param>
        /// <param name="valueEquality">Equality comparer for values</param>
        /// <returns>Whether the two dictionaries are equal.</returns>
        public static bool Equals1<TKey, TValue>(this IDictionary<TKey, TValue> lhs, IDictionary<TKey, TValue> rhs, IEqualityComparer<TValue> valueEquality)
        {
            if (ReferenceEquals(lhs, null))
            {
                return ReferenceEquals(rhs, null);
            }

            if (ReferenceEquals(rhs, null))
            {
                return ReferenceEquals(lhs, null);
            }

            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (lhs.Count != rhs.Count)
            {
                return false;
            }

            foreach (var key in lhs.Keys)
            {
                TValue rhsValue;
                if (!rhs.TryGetValue(key, out rhsValue))
                {
                    return false;
                }

                if (!valueEquality.Equals(lhs[key], rhsValue))
                {
                    return false;
                }
            }
            return true;
        }
    
        #endregion
    }

    /// <summary>
    /// Equality comparer for array of T using Equals1 method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayEqualityComparer<T> : IEqualityComparer<T[]>
    {
        /// <summary>
        /// Element equality
        /// </summary>
        public IEqualityComparer<T> ElementEqualityComparer
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor using default element equality
        /// </summary>
        public ArrayEqualityComparer()
            : this(EqualityComparer<T>.Default)
        { }

        /// <summary>
        /// Constructor using a given element equality
        /// </summary>
        /// <param name="elementEqualityComparer"></param>
        public ArrayEqualityComparer(IEqualityComparer<T> elementEqualityComparer)
        {
            ElementEqualityComparer = elementEqualityComparer;
        }

        /// <summary>
        /// Implements IEqualityComparer.Equals
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T[] x, T[] y)
        {
            return x.Equals1(y, ElementEqualityComparer);
        }

        /// <summary>
        /// Implements IEqualityComparer.GetHashCode
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int GetHashCode(T[] x)
        {
            var hashcode = 0;
            foreach (var e in x)
            {
                hashcode ^= ElementEqualityComparer.GetHashCode(e);
            }
            return hashcode;
        }
    }

    /// <summary>
    /// IEqualityComparer for floats with user-specified tolerance.
    /// </summary>
    public class FloatEqualityComparer : IEqualityComparer<float>
    {
        /// <summary>
        /// Default tolerance
        /// </summary>
        public static float DefaultTolerance
        {
            get
            {
                return 1e-5f;
            }
        }

        /// <summary>
        /// Tolerance used during equality comparison
        /// </summary>
        public float Tolerance
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs FloatEqualityComparer with default tolerance.
        /// </summary>
        public FloatEqualityComparer()
            : this(DefaultTolerance)
        { }

        /// <summary>
        /// Constructs FloatEqualityComparer with user-given tolerance.
        /// </summary>
        /// <param name="tolerance"></param>
        public FloatEqualityComparer(float tolerance)
        {
            Tolerance = tolerance;
        }

        /// <summary>
        /// Implements IEqualityComparer.Equals
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(float x, float y)
        {
            return Math.Abs(x - y) <= Tolerance;
        }

        /// <summary>
        /// Implements IEqualityComparer.GetHashCode
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int GetHashCode(float x)
        {
            return x.GetHashCode();
        }
    }

    /// <summary>
    /// IEqualityComparer for floats with user-specified tolerance.
    /// </summary>
    public class DoubleEqualityComparer : IEqualityComparer<double>
    {
        /// <summary>
        /// Default tolerance
        /// </summary>
        public static double DefaultTolerance
        {
            get
            {
                return 1e-5;
            }
        }

        /// <summary>
        /// Tolerance used during equality comparison
        /// </summary>
        public double Tolerance
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs DoubleEqualityComparer with default tolerance.
        /// </summary>
        public DoubleEqualityComparer()
            : this(DefaultTolerance)
        { }

        /// <summary>
        /// Constructs DoubleEqualityComparer with user-given tolerance.
        /// </summary>
        /// <param name="tolerance"></param>
        public DoubleEqualityComparer(double tolerance)
        {
            Tolerance = tolerance;
        }

        /// <summary>
        /// Implements IEqualityComparer.Equals
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(double x, double y)
        {
            return Math.Abs(x - y) <= Tolerance;
        }

        /// <summary>
        /// Implements IEqualityComparer.GetHashCode
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int GetHashCode(double x)
        {
            return x.GetHashCode();
        }
    }

    /// <summary>
    /// EqualityComparer that uses Object.ReferenceEquals to compare items.
    /// </summary>
    /// <typeparam name="T">Type of items to compare.</typeparam>
    public class ReferenceEqualityComparer<T> : EqualityComparer<T>
    {
        private static readonly ReferenceEqualityComparer<T> ReferenceEqualityComparerInstance = new ReferenceEqualityComparer<T>();

        /// <summary>
        /// Default singleton instance of this class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
            Justification = "Yes, it's true that type T cannot be inferred while using this static instance, but in this case, it's ok.")]
        public static ReferenceEqualityComparer<T> Instance
        {
            get
            {
                return ReferenceEqualityComparerInstance;
            }
        }

        // Make constructor private so that clients must use Instance.
        private ReferenceEqualityComparer()
        {
        }

        /// <summary>
        /// Equates two objects of type T using ReferenceEquals.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool Equals(T x, T y)
        {
            return ReferenceEquals(x, y);
        }

        /// <summary>
        /// Hash code using the usual Object.GetHashCode when applied to type T.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override int GetHashCode(T x)
        {
            return x.GetHashCode();
        }
    }
}
