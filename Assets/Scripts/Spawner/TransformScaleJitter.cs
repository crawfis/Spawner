using CrawfisSoftware.PCG;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class TransformScaleJitter : ITransformModifier
    {
        private readonly RandomVector3WithinRange randomVector;

        public TransformScaleJitter(Vector3 maxScale) : this(-maxScale, maxScale) { }
        public TransformScaleJitter(Vector3 minScale, Vector3 maxScale)
        {
            randomVector = new RandomVector3WithinRange(minScale, maxScale);
        }
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localScale = randomVector.GetNext();
        }
    }
}
