using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Creates new game objects using an existing game object as a template.
    /// </summary>
    public class PrefabGeneratorPooled : IPrefabGeneratorAsync
    {
        private readonly IPooler<GameObject> prefabPool;
        private readonly GameObject prefab;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefab">The game object to use as a template for new game objects.</param>
        public PrefabGeneratorPooled(GameObject prefab, int defaultCapacity = 100, int maxPersistentSize = 10000)
        {
            prefabPool = new GameObjectPooler(prefab);
            this.prefab = prefab;
        }

        /// <inheritdoc/>
        public async Task<GameObject> CreateAsync(Vector3 position, SpawnerAndModifier spawner, int counter)
        {
            GameObject newGameObject = prefabPool.Get();
            newGameObject.transform.localPosition = position;
            return await Task.FromResult<GameObject>(newGameObject);
        }
    }
}