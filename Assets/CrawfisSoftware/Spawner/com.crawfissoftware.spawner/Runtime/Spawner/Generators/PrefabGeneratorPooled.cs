using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Creates game objects using an existing game object as a template. These are then pooled and reused.
    /// </summary>
    /// <remarks>Note: Only works with static objects as there is no reset. Could handle reset on isActive.</remarks>
    public class PrefabGeneratorPooled : IPrefabGeneratorAsync
    {
        private readonly IPooler<GameObject> _prefabPool;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="assetGenerator">The game object generator that provides new game object instances.</param>
        public PrefabGeneratorPooled(IPrefabGeneratorAsync assetGenerator, int defaultCapacity = 100, int maxPersistentSize = 10000)
        {
            _prefabPool = new GameObjectPooler(assetGenerator, defaultCapacity, maxPersistentSize);
        }

        /// <inheritdoc/>
        public async Task<GameObject> CreateAsync(Vector3 position, SpawnerAndModifier spawner, int counter)
        {
            GameObject newGameObject = _prefabPool.Get();
            newGameObject.transform.localPosition = position;
            return await Task.FromResult<GameObject>(newGameObject);
        }

        public void Release(GameObject asset)
        {
            _prefabPool.Release(asset);
        }
    }
}