using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Apply a random rotation.
    /// </summary>
    public class TransformRandomRotation : ITransformModifier
    {
        /// <inheritdoc/>
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localRotation = UnityEngine.Random.rotationUniform;
        }
    }
}
