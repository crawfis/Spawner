using CrawfisSoftware.PCG;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class TransformPositionJitter : ITransformModifier
    {
        private readonly RandomVector3WithinRange randomVector;

        public TransformPositionJitter(Vector3 maxPosition) : this(-maxPosition, maxPosition) { }
        public TransformPositionJitter(Vector3 minPosition, Vector3 maxPosition)
        {
            randomVector = new RandomVector3WithinRange(minPosition, maxPosition);
        }
        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localPosition += randomVector.GetNext();
        }
    }
}
