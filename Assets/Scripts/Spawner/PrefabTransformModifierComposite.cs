using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Contains a list of ITransformModifiers to apply to a prefab.
    /// </summary>
    public class PrefabTransformModifierComposite : IPrefabModifierAsync
    {
        private readonly List<ITransformModifier> transformModifiers = new List<ITransformModifier>();

        /// <summary>
        /// Add a transform modifier.
        /// </summary>
        /// <param name="transformModifier">A ITransformModifier to add to the list of current modifiers.</param>
        public void AddTransformModifier(ITransformModifier transformModifier)
        {
            transformModifiers.Add(transformModifier);
        }

        /// <inheritdoc/>
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
