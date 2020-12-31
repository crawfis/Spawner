using UnityEngine;

namespace CrawfisSoftware.PCG
{
    public class RandomColorWithinRange
    {
        private readonly Color minValues;
        private readonly Color maxValues;

        public RandomColorWithinRange(Color minValues, Color maxValues)
        {
            this.minValues = minValues;
            this.maxValues = maxValues;
        }
        public Color GetNext()
        {
            float r = UnityEngine.Random.Range(minValues.r, maxValues.r);
            float g = UnityEngine.Random.Range(minValues.g, maxValues.g);
            float b = UnityEngine.Random.Range(minValues.b, maxValues.b);
            float a = UnityEngine.Random.Range(minValues.a, maxValues.a);
            return new Color(r, g, b, a);
        }
    }
}