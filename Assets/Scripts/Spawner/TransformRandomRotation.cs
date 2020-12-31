using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class TransformRandomRotation : ITransformModifier
    {
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localRotation = UnityEngine.Random.rotationUniform;
        }
    }
}
