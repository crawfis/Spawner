using UnityEngine;

namespace CrawfisSoftware
{

    /// <summary>
    /// IPooler implementation for GameObjects (prefabs)
    /// </summary>
    public class GameObjectPooler : PoolerBase<GameObject>
    {
        public GameObjectPooler(GameObject prefab, int initialSize = 100, int maxPersistentSize = 10000, bool collectionChecks = false)
            : base(prefab, initialSize, maxPersistentSize, collectionChecks)
        {
        }

        protected override GameObject CreateNewPoolInstance() => Object.Instantiate(_prefab);
        protected override void GetPoolInstance(GameObject poolObject) => poolObject.SetActive(true);
        protected override void ReleasePoolInstance(GameObject poolObject) => poolObject.SetActive(false);
        protected override void DestroyPoolInstance(GameObject poolObject) => Object.Destroy(poolObject);
    }
}