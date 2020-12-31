using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabTransformModifierComposite : IPrefabModifierAsync
    {
        private readonly List<ITransformModifier> transformModifiers = new List<ITransformModifier>();

        public void AddTransformModifier(ITransformModifier transformModifier)
        {
            transformModifiers.Add(transformModifier);
        }
        public async Task ApplyAsync(GameObject prefab)
        {
            Transform transform = prefab.transform;
            foreach(ITransformModifier modifier in transformModifiers)
            {
                modifier.ModifyTransform(transform);
            }
            await Task.CompletedTask;
        }
    }
}
