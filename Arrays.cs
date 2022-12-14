/* *********************************************************************
 * Date: 11 Jun 2012
 * Created by: Zoltan Juhasz
 * E-Mail: forge@jzo.hu
***********************************************************************/

using System;
using System.Collections;

namespace Forge.Shared
{

    /// <summary>
    /// Array operation(s)
    /// </summary>
    public static class Arrays
    {

        /// <summary>
        /// Compare the two arrays
        /// </summary>
        /// <param name="a">Array A</param>
        /// <param name="b">Array B</param>
        /// <returns>True if the two array are equals, otherwise False.</returns>
        /// <example>
        /// <code>
        ///   int[] a = new int[] { 1, 2, 3, 4 };
        ///   int[] b = new int[] { 1, 2, 3, 4 };
        ///   Assert.IsTrue(Arrays.DeepEquals(a, b));
        /// </code>
        /// </example>
        public static bool DeepEquals(Array
#if NET40 || NETSTANDARD2_0
#else
            ? 
#endif
            a, Array
#if NET40 || NETSTANDARD2_0
#else
            ? 
#endif
            b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (a.Length != b.Length)
            {
                return false;
            }

            bool result = true;

            IEnumerator enA = a.GetEnumerator();
            IEnumerator enB = b.GetEnumerator();

            while (enA.MoveNext() && enB.MoveNext())
            {
                if (enA.Current == null && enB.Current == null)
                {
                }
                else if (enA.Current == null || enB.Current == null)
                {
                    result = false;
                    break;
                }
                else if (enA is Array && enB is Array)
                {
                    result = DeepEquals((Array)enA, (Array)enB);
                    if (!result)
                    {
                        break;
                    }
                }
                else if (enA.Current == enB.Current)
                {
                }
                else if (enA.Current.Equals(enB.Current))
                {
                }
                else
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

    }

}
