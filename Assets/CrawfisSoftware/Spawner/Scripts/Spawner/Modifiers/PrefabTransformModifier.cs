using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Modify a transform. Supports both the IPrefabModifierAsync and ITransformModifier interfaces.
    /// </summary>
    public class PrefabTransformModifier : IPrefabModifierAsync, ITransformModifier
    {
        private readonly Vector3 offset;
        private readonly Vector3 eulerAngles;
        private readonly Vector3 scale;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="localOffset">An offset to apply to the local position.</param>
        /// <param name="localRotation">A set of Euler angles to set the local rotation.</param>
        /// <param name="localScale">A set of scales to set the local scale.</param>
        public PrefabTransformModifier(Vector3 localOffset, Vector3 localRotation, Vector3 localScale)
        {
            this.offset = localOffset;
            this.eulerAngles = localRotation;
            this.scale = localScale;
        }

        /// <inheritdoc/>
        public async Task ApplyAsync(GameObject prefab)
        {
            ModifyTransform(prefab.transform);
            await Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localPosition += offset;
            currentTransform.localEulerAngles = eulerAngles;
            currentTransform.localScale = scale;
        }
    }
}