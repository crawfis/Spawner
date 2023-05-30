using CrawfisSoftware.PCG;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Rotate the game object randomly within a fixed range.
    /// </summary>
    public class TransformRotationJitter : ITransformModifier
    {
        private readonly RandomVector3WithinRange randomVector;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="minEulerAngleDeltas">The minimum euler angles.</param>
        /// <param name="maxEulerAngleDeltas">The maximum euler angles.</param>
        public TransformRotationJitter(Vector3 minEulerAngleDeltas, Vector3 maxEulerAngleDeltas)
        {
            randomVector = new RandomVector3WithinRange(minEulerAngleDeltas, maxEulerAngleDeltas);
        }

        /// <inheritdoc/>
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localEulerAngles += randomVector.GetNext();
        }
    }
}
