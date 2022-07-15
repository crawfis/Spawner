using UnityEngine;

namespace CrawfisSoftware.PCG
{
    /// <summary>
    /// Random Vector3 class. Provides random vectors between two vectors (unnormalized by default).
    /// </summary>
    public class RandomVector3WithinRange
    {
        private readonly Vector3 minValues;
        private readonly Vector3 maxValues;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minValues">The minimum bound of the range (in each component).</param>
        /// <param name="maxValues">The maximum bound for the range.</param>
        public RandomVector3WithinRange(Vector3 minValues, Vector3 maxValues)
        {
            this.minValues = minValues;
            this.maxValues = maxValues;
        }

        /// <summary>
        /// Get a random Vector3 between the two bounds.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetNext()
        {
            float x = UnityEngine.Random.Range(minValues.x, maxValues.x);
            float y = UnityEngine.Random.Range(minValues.y, maxValues.y);
            float z = UnityEngine.Random.Range(minValues.z, maxValues.z);
            return new Vector3(x, y, z);
        }
    }
}