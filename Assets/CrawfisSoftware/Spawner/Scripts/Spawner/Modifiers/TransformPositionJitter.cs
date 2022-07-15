using CrawfisSoftware.PCG;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Translate the local position by a random amount.
    /// </summary>
    public class TransformPositionJitter : ITransformModifier
    {
        private readonly RandomVector3WithinRange randomVector;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxOffset">An unsigned maximum amount of jitter.</param>
        public TransformPositionJitter(Vector3 maxOffset) : this(-maxOffset, maxOffset) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minOffset">The minimum amount of jitter.</param>
        /// <param name="maxOffset">The maximum amount of jitter.</param>
        public TransformPositionJitter(Vector3 minOffset, Vector3 maxOffset)
        {
            randomVector = new RandomVector3WithinRange(minOffset, maxOffset);
        }

        /// <inheritdoc/>
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localPosition += randomVector.GetNext();
        }
    }
}
