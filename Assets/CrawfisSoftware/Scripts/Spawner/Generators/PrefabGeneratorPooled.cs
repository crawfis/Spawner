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
        private readonly ObjectPool<GameObject> prefabPool;
        private readonly GameObject prefab;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefab">The game object to use as a template for new game objects.</param>
        public PrefabGeneratorPooled(GameObject prefab, int defaultCapacity = 100, int maxPersistentSize = 10000)
        {
            prefabPool = new ObjectPool<GameObject>(CreateNewPooledPrefab, GetPooledPrefab, ReleasePooledPrefab, 
                DestroyPooledPrefab, true, defaultCapacity, maxPersistentSize);
            this.prefab = prefab;
        }

        /// <inheritdoc/>
        public async Task<GameObject> CreateAsync(Vector3 position, SpawnerAndModifier spawner, int counter)
        {
            GameObject newGameObject = prefabPool.Get();
            newGameObject.transform.localPosition = position;
            return await Task.FromResult<GameObject>(newGameObject);
        }
        private GameObject CreateNewPooledPrefab() => UnityEngine.Object.Instantiate<GameObject>(prefab);
        private void GetPooledPrefab(GameObject prefab) => prefab.gameObject.SetActive(true);
        private void ReleasePooledPrefab(GameObject prefab) => prefab.gameObject.SetActive(false);
        private void DestroyPooledPrefab(GameObject prefab) => UnityEngine.Object.Destroy(prefab);
    }
}