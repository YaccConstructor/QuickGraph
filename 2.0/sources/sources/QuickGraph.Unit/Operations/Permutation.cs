using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace QuickGraph.Operations
{
    /// <summary>
    /// A class to generate permutations.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class can generate any sequence of permutation of order <see cref="Order"/>.
    /// The <see cref="GetSuccessor"/> method returns the next permutation, while
    /// <see cref="GetSuccessors"/> can be used to iterates all the rest of the permutations.
    /// </para>
    /// <para>
    /// The permutation can be applied to an array using <see cref="ApplyTo"/>, it can also
    /// be inverted using <see cref="Inverse"/>.
    /// </para>
    /// <para>
    /// This class was extracted from
    /// <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnnetsec/html/permutations.asp">
    /// <em>Using Permutations in .NET for Improved Systems Security</em> by
    /// Dr. James McCaffrey.
    /// </a>
    /// </para>
    /// </remarks>
    public sealed class Permutation
    {
        private int[] data = null;
        private int order = 0;

        /// <summary>
        /// Creates a new idenity permutation
        /// </summary>
        /// <param name="n">
        /// order of the new permutation
        /// </param>
        public Permutation(int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException("n cannot be negative or zero");
            this.data = new int[n];
            for (int i = 0; i < n; ++i)
            {
                this.data[i] = i;
            }
            this.order = n;
        }

        /// <summary>
        /// Creates the <paramref name="k"/>-th permutation of
        /// order <paramref name="n"/>.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        public Permutation(int n, int k)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException("n cannot be negative or zero");
            if (k <= 0)
                throw new ArgumentOutOfRangeException("k cannot be negative or zero");

            this.data = new int[n];
            this.order = this.data.Length;

            // Step #1 - Find factoradic of k
            int[] factoradic = new int[n];

            for (int j = 1; j <= n; ++j)
            {
                factoradic[n - j] = k % j;
                k /= j;
            }

            // Step #2 - Convert factoradic to permuatation
            int[] temp = new int[n];

            for (int i = 0; i < n; ++i)
            {
                temp[i] = ++factoradic[i];
            }

            this.data[n - 1] = 1;  // right-most element is set to 1.

            for (int i = n - 2; i >= 0; --i)
            {
                this.data[i] = temp[i];
                for (int j = i + 1; j < n; ++j)
                {
                    if (this.data[j] >= this.data[i])
                        ++this.data[j];
                }
            }
            for (int i = 0; i < n; ++i)  // put in 0-based form
            {
                --this.data[i];
            }
        }  // Permutation(n,k)

        private Permutation(int[] a)
        {
            if (a.Length == 0)
                throw new ArgumentException("Order cannot be zero");
            this.data = new int[a.Length];
            a.CopyTo(this.data, 0);
            this.order = a.Length;

            this.CheckPermutation();
        }

        /// <summary>
        /// Gets the order of the permutation
        /// </summary>
        /// <value></value>
        public int Order
        {
            get
            {
                return this.order;
            }
        }

        /// <summary>
        /// Checks that the permutation is correct
        /// </summary>
        private void CheckPermutation()
        {
            if (this.data.Length != this.order)
                throw new Exception("Data.Length is not equal to the Order");

            bool[] checks = new bool[this.data.Length];
            for (int i = 0; i < this.order; ++i)
            {
                if (this.data[i] < 0 || this.data[i] >= this.order)
                    throw new Exception("Value out of range at index " + i.ToString());  // value out of range

                if (checks[this.data[i]] == true)
                    throw new Exception("Duplicate value at index " + i.ToString());  // value out of range
                checks[this.data[i]] = true;
            }
        }

        /// <summary>
        /// Converts the permutation to a string representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringWriter sb = new StringWriter();
            sb.Write("(");
            for (int i = 0; i < this.order; ++i)
            {
                sb.Write("{0} ", this.data[i]);
            }
            sb.Write(")");

            return sb.ToString();
        }

        /// <summary>
        /// Applis the permutation to the array 
        /// </summary>
        /// <param name="arr">
        /// A <see cref="Object"/> array of Length equal 
        /// to <see cref="Order"/>.</param>
        /// <returns>
        /// A new array containing the permutated element of <paramref name="arr"/>
        /// </returns>
        public T[] ApplyTo<T>(T[] arr)
        {
            if (arr.Length != this.order)
                throw new ArgumentException("array Length is equal to the permutation order");

            T[] result = new T[arr.Length];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = arr[this.data[i]];
            }

            return result;
        }

        public void ApplyTo<T>(IList<T> arr)
        {
            if (arr.Count != this.order)
                throw new ArgumentException("array Count is equal to the permutation order");

            T[] result = new T[arr.Count];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = arr[this.data[i]];
            }

            for (int i = 0; i < result.Length; ++i)
            {
                arr[i] = result[i];
            }
        }

        /// <summary>
        /// Creates the inverse of the permutation.
        /// </summary>
        /// <returns></returns>
        public Permutation Inverse()
        {
            int[] inverse = new int[this.order];

            for (int i = 0; i < inverse.Length; ++i)
            {
                inverse[this.data[i]] = i;
            }

            return new Permutation(inverse);
        }

        /// <summary>
        /// Creates the next permutation in lexicographic order.
        /// </summary>
        /// <returns>
        /// The next <see cref="Permutation"/> instance if there remain any;
        /// otherwize a null reference.
        /// </returns>
        public Permutation GetSuccessor()
        {
            Permutation result = new Permutation(this.order);

            int left, right;

            for (int k = 0; k < result.order; ++k)  // Step #0 - copy current data into result
            {
                result.data[k] = this.data[k];
            }

            left = result.order - 2;  // Step #1 - Find left value 
            while ((result.data[left] > result.data[left + 1]) && (left >= 1))
            {
                --left;
            }
            if ((left == 0) && (this.data[left] > this.data[left + 1]))
                return null;

            right = result.order - 1;  // Step #2 - find right; first value > left
            while (result.data[left] > result.data[right])
            {
                --right;
            }

            int temp = result.data[left];  // Step #3 - swap [left] and [right]
            result.data[left] = result.data[right];
            result.data[right] = temp;


            int i = left + 1;              // Step #4 - order the tail
            int j = result.order - 1;

            while (i < j)
            {
                temp = result.data[i];
                result.data[i++] = result.data[j];
                result.data[j--] = temp;
            }

            return result;
        }

        /// <summary>
        /// Gets an enumerable collection of <see cref="Permutation"/> successors.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Permutation> GetSuccessors()
        {
            Permutation current = this.GetSuccessor();
            while (current != null)
            {
                yield return current;
                current = current.GetSuccessor();
            }
        }
    }
}
