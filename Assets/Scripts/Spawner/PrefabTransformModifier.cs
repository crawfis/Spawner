using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabTransformModifier : IPrefabModifierAsync, ITransformModifier
    {
        private readonly Vector3 offset;
        private readonly Vector3 eulerAngles;
        private readonly Vector3 scale;

        public PrefabTransformModifier(Vector3 localOffset, Vector3 localRotation, Vector3 localScale)
        {
            this.offset = localOffset;
            this.eulerAngles = localRotation;
            this.scale = localScale;
        }

        public async Task ApplyAsync(GameObject prefab)
        {
            ModifyTransform(prefab.transform);
            await Task.CompletedTask;
        }

        public void ModifyTransform(Transform currentTransform)
        {
            currentTransform.localPosition += offset;
            currentTransform.localEulerAngles = eulerAngles;
            currentTransform.localScale = scale;
        }
    }
}