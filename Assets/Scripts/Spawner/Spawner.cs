using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class Spawner : IPrefabModifierAsync
    {
        private readonly IPrefabSelectorAsync prefabSelector;
        private readonly IList<IPrefabModifierAsync> prefabModifiers;
        private int count = 0;

        public Spawner(IPrefabSelectorAsync prefabSelector, IList<IPrefabModifierAsync> prefabModifiers)
        {
            this.prefabSelector = prefabSelector;
            this.prefabModifiers = prefabModifiers;
        }

        public IEnumerable<GameObject> SpawnStream(IEnumerable<Vector3> positionGenerator, Transform parentTransform)
        {
            foreach(Vector3 position in positionGenerator)
            {
                System.Threading.Tasks.Task<GameObject> task = SpawnAsync(position, parentTransform);
                yield return task.GetAwaiter().GetResult();
                //task.Wait();
                //yield return task.Result;
            }
        }

        public virtual async System.Threading.Tasks.Task<GameObject> SpawnAsync(Vector3 position, Transform parentTransform)
        {
            GameObject newInstance = await prefabSelector.CreateAsync(position, this, count++);
            newInstance.transform.SetParent(parentTransform, false);
            foreach (IPrefabModifierAsync prefabModifier in prefabModifiers)
            {
                await prefabModifier.ApplyAsync(newInstance);
            }
            return newInstance;
        }

        public async System.Threading.Tasks.Task ApplyAsync(GameObject prefab)
        {
            await SpawnAsync(Vector3.zero, prefab.transform);
        }
    }
}