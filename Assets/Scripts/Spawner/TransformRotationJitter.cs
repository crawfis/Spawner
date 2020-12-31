using CrawfisSoftware.PCG;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class TransformRotationJitter : ITransformModifier
    {
        private readonly RandomVector3WithinRange randomVector;

        public TransformRotationJitter(Vector3 minEulerAngleDeltas, Vector3 maxEulerAngleDeltas)
        {
            randomVector = new RandomVector3WithinRange(minEulerAngleDeltas, maxEulerAngleDeltas);
        }
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localEulerAngles += randomVector.GetNext();
        }
    }
}
