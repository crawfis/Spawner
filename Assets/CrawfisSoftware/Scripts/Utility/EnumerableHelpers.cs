using System.Collections.Generic;
using System.Linq;

namespace CrawfisSoftware
{
    public static class EnumerableHelpers
    {
        /// <summary>
        /// Enumerates a list in a random order.
        /// </summary>
        /// <typeparam name="T">Generic type of the list elements</typeparam>
        /// <param name="finiteList">A list.</param>
        /// <param name="random">Optional random number generator.</param>
        /// <returns>An enumeration of the list elements.</returns>
        public static IEnumerable<T> Shuffle<T>(IList<T> finiteList, System.Random random = null)
        {
            if (random == null)
                random = new System.Random();

            int count = finiteList.Count;
            var permutation = new int[count];
            for (int i = 0; i < count; i++)
            {
                int index = random.Next(i + 1);
                if (index != i)
                    permutation[i] = permutation[index];
                permutation[index] = i;
            }

            for (int j = 0; j < count; j++)
            {
                yield return finiteList[permutation[j]];
            }
        }

        /// <summary>
        /// Repeatedly enumerates a set
        /// </summary>
        /// <param name="possibleFiniteSet">A stream of objects of type T.</param>
        /// <returns>An infinite stream with the finiteSet repeated.</returns>
        /// <seealso cref="TakeWithRepeats(int, IEnumerable{T})"/>
        public static IEnumerable<T> MakeInfinite<T>(IEnumerable<T> possibleFiniteSet)
        {
            while (true)
            {
                foreach (T position in possibleFiniteSet)
                {
                    yield return position;
                }
            }
        }

        /// <summary>
        /// Returns either a finite set or the first N items
        /// </summary>
        /// <param name="stream">A stream of objects of type T.</param>
        /// <returns>A finite stream.</returns>
        /// <seealso cref="TakeWithRepeats(int, IEnumerable{T})"/>
        public static IEnumerable<T> MakeFinite<T>(IEnumerable<T> stream, int maxSetSize = 1000)
        {
            int count = 0;
            foreach (T position in stream)
            {
                if (count >= maxSetSize) break;
                yield return position;
                count++;
            }
        }

        /// <summary>
        /// Creates a finite stream by repeating if necessary another stream.
        /// </summary>
        /// <param name="numberOfSamples">The max number of objects to enumerate.</param>
        /// <param name="stream">A stream of type T</param>
        /// <returns>A stream of type T</returns>
        /// <remarks>Equivalent to using LINQ's Take(numberOfSamples) with MakeInfinite(finiteSet).</remarks>
        public static IEnumerable<T> TakeWithRepeats<T>(int numberOfSamples, IEnumerable<T> stream)
        {
            return MakeInfinite(stream).Take(numberOfSamples);
        }
    }
}
