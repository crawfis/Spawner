using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PooledSpawner : PoolerBaseAsync<GameObject>
    {
        private readonly ISpawner _spawner;

        public PooledSpawner(SpawnerAndModifier spawner, int initialSize = 100, int maxPersistentSize = 10000, bool collectionChecks = false)
            : base(initialSize, maxPersistentSize, collectionChecks)
        {
            _spawner = spawner;
        }

        protected override async Task<GameObject> CreateNewPoolInstanceAsync()
        {
            GameObject asset = await _spawner.SpawnAsync(Vector3.zero, null);
            return asset;
        }

        protected override async Task DestroyPoolInstanceAsync(GameObject poolObject)
        {
            UnityEngine.Object.Destroy(poolObject);
            await Task.CompletedTask;
        }

        protected override async Task GetPoolInstanceAsync(GameObject poolObject)
        {
            poolObject.SetActive(true);
            await Task.CompletedTask;
        }

        protected override async Task ReleasePoolInstanceAsync(GameObject poolObject)
        {
            poolObject.SetActive(false);
            await Task.CompletedTask;
        }
    }
}