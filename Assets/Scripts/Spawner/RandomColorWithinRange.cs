using UnityEngine;

namespace CrawfisSoftware.PCG
{
    /// <summary>
    /// Class to generate random colors between two color values (in RGBA space by default).
    /// </summary>
    public class RandomColorWithinRange
    {
        private readonly Color color1;
        private readonly Color color2;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color1">A color.</param>
        /// <param name="color2">A color</param>
        public RandomColorWithinRange(Color color1, Color color2)
        {
            this.color1 = color1;
            this.color2 = color2;
        }

        /// <summary>
        /// Get a random color.
        /// </summary>
        /// <returns>A Color.</returns>
        public Color GetNext()
        {
            float r = UnityEngine.Random.Range(color1.r, color2.r);
            float g = UnityEngine.Random.Range(color1.g, color2.g);
            float b = UnityEngine.Random.Range(color1.b, color2.b);
            float a = UnityEngine.Random.Range(color1.a, color2.a);
            return new Color(r, g, b, a);
        }
    }
}