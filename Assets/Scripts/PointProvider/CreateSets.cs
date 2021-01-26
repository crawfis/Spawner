using System;
using System.Collections.Generic;
using System.Linq;

namespace CrawfisSoftware
{
    public static class CreateSets
    {
        public static IEnumerable<IEnumerable<T>> SequentialSubsets<T>(IEnumerable<T> stream, int subsetSize)
        {
            return SequentialSubsets<T>(stream, new ConstantValueFunc(subsetSize).ConstantValue);
        }
        public static IEnumerable<IEnumerable<T>> SequentialSubsets<T>(IEnumerable<T> stream, Func<int, int> subSetSize)
        {
            int iteration = 0;
            int currentRequestedSize = subSetSize(iteration);
            var outputSet = new List<T>(currentRequestedSize);
            int currentSetSize = 0;
            foreach (T item in stream)
            {
                if (currentSetSize == currentRequestedSize)
                {
                    yield return outputSet;
                    currentRequestedSize = subSetSize(iteration);
                    outputSet = new List<T>(currentRequestedSize);
                }
                outputSet.Add(item);
            }
            yield return outputSet;
        }
        public static IEnumerable<IEnumerable<T>> RandomSubsets<T>(IList<T> stream, int fixedSubsetSize, System.Random random)
        {
            return RandomSubsets<T>(stream, new ConstantValueFunc(fixedSubsetSize).ConstantValue, random);
        }

        public static IEnumerable<IEnumerable<T>> RandomSubsets<T>(IList<T> stream, Func<int, int> subSetSize, System.Random random)
        {
            if (random == null) random = new System.Random();
            int iteration = 0;
            int currentRequestedSize = subSetSize(iteration);
            var outputSet = new List<T>(currentRequestedSize);
            int currentSetSize = 0;
            var shuffledStream = EnumerableHelpers.Shuffle(stream, random);
            foreach (T item in stream)
            {
                if (currentSetSize == currentRequestedSize)
                {
                    yield return outputSet;
                    currentRequestedSize = subSetSize(iteration);
                    outputSet = new List<T>(currentRequestedSize);
                }
                outputSet.Add(item);
            }
            yield return outputSet;
        }

        private class ConstantValueFunc
        {
            private readonly int constantValue;
            public ConstantValueFunc(int constantValue)
            {
                this.constantValue = constantValue;
            }

            public int ConstantValue(int iteration)
            {
                return constantValue;
            }
        }
    }
}