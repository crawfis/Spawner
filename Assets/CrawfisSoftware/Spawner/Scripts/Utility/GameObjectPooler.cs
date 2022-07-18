using UnityEngine;

namespace CrawfisSoftware.Spawner
{

    /// <summary>
    /// IPooler implementation for GameObjects (prefabs)
    /// </summary>
    public class GameObjectPooler : PoolerBase<GameObject>
    {
        private readonly IPrefabGeneratorAsync _prefab;
        private int _counter = 0;

        public GameObjectPooler(IPrefabGeneratorAsync prefab, int initialSize = 100, int maxPersistentSize = 10000, bool collectionChecks = false)
            : base(initialSize, maxPersistentSize, collectionChecks)
        {
            this._prefab = prefab;
        }

        protected override GameObject CreateNewPoolInstance() { 
            var task = _prefab.CreateAsync(Vector3.zero, null, _counter++); 
            var asset = task.GetAwaiter().GetResult();
            return asset; 
        }
        protected override void GetPoolInstance(GameObject poolObject) => poolObject.SetActive(true);
        protected override void ReleasePoolInstance(GameObject poolObject) => poolObject.SetActive(false);
        protected override void DestroyPoolInstance(GameObject poolObject) => Object.Destroy(poolObject);
    }
}