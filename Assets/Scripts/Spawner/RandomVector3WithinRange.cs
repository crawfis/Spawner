using UnityEngine;

namespace CrawfisSoftware.PCG
{
    public class RandomVector3WithinRange
    {
        private readonly Vector3 minValues;
        private readonly Vector3 maxValues;

        public RandomVector3WithinRange(Vector3 minValues, Vector3 maxValues)
        {
            this.minValues = minValues;
            this.maxValues = maxValues;
        }
        public Vector3 GetNext()
        {
            float x = UnityEngine.Random.Range(minValues.x, maxValues.x);
            float y = UnityEngine.Random.Range(minValues.y, maxValues.y);
            float z = UnityEngine.Random.Range(minValues.z, maxValues.z);
            return new Vector3(x, y, z);
        }
    }
}