using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Flexible Spawner of GameObjects. Instances are also IPrefabModifierAsync 
    /// </summary>
    public class Spawner : IPrefabModifierAsync, ISpawner
    {
        private readonly IPrefabGeneratorAsync prefabSelector;
        private readonly IList<IPrefabModifierAsync> prefabModifiers;
        private int count = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prefabSelector">Provides the new GameObjects when requested.</param>
        /// <param name="prefabModifiers">Used to perform any modifications after a game object is created.</param>
        public Spawner(IPrefabGeneratorAsync prefabSelector, IList<IPrefabModifierAsync> prefabModifiers)
        {
            this.prefabSelector = prefabSelector;
            this.prefabModifiers = prefabModifiers;
        }

        /// <summary>
        /// Creates a stream (lazily) of new game objects.
        /// </summary>
        /// <param name="positionGenerator">Provides a stream of positions that should be used in the creation process.</param>
        /// <param name="parentTransform">The transform that all created game objects should be parented to.</param>
        /// <returns></returns>
        public IEnumerable<GameObject> SpawnStream(IEnumerable<Vector3> positionGenerator, Transform parentTransform)
        {
            foreach (Vector3 position in positionGenerator)
            {
                System.Threading.Tasks.Task<GameObject> task = SpawnAsync(position, parentTransform);
                yield return task.GetAwaiter().GetResult();
                //task.Wait();
                //yield return task.Result;
            }
        }

        /// <summary>
        /// Asynchronously create a new game object at the specified position.
        /// </summary>
        /// <param name="position">The position that the new game object should be placed at.</param>
        /// <param name="parentTransform">A parent transform.</param>
        /// <returns>A newly created GameObject (wrapped in a Task).</returns>
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

        /// <inheritdoc/>
        public async System.Threading.Tasks.Task ApplyAsync(GameObject prefab)
        {
            await SpawnAsync(Vector3.zero, prefab.transform);
        }
    }
}