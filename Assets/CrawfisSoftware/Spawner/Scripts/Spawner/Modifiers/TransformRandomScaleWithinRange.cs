using CrawfisSoftware.PCG;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Give the transform a random rotation within a range.
    /// </summary>
    public class TransformRandomScaleWithinRange : ITransformModifier
    {
        private readonly RandomVector3WithinRange randomVector;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minScale">Minimum scale values.</param>
        /// <param name="maxScale">Maximum scale values.</param>
        public TransformRandomScaleWithinRange(Vector3 minScale, Vector3 maxScale)
        {
            randomVector = new RandomVector3WithinRange(minScale, maxScale);
        }

        /// <inheritdoc/>
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localScale = randomVector.GetNext();
        }
    }
}
