using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public class PointProviderRandomXZ : IPositionGenerator
    {
        private readonly float yValue;
        private readonly System.Random random;

        public PointProviderRandomXZ(float yValue = 0f, System.Random random = null)
        {
            this.yValue = yValue;
            this.random = random;
            if (random == null)
                this.random = new System.Random();
        }
 
        public IList<Vector3> GetNext()
        {
            return new Vector3[1] { new Vector3((float)random.NextDouble(), yValue, (float)random.NextDouble()) };
        }
    }
}