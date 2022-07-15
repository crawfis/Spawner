using UnityEngine.Pool;

namespace CrawfisSoftware
{
    /// <summary>
    /// Abstract base class for some implementations of IPooler
    /// </summary>
    /// <typeparam name="T">The type of instances in the pool.</typeparam>
    public abstract class PoolerBase<T> : IPooler<T> where T : class
    {
        protected ObjectPool<T> _pool;

        public T Get() => _pool.Get();
        public void Release(T poolObject) => _pool.Release(poolObject);

        public PoolerBase(int initialSize = 100, int maxPersistentSize = 10000, bool collectionChecks = false)
        {
            InitPool(initialSize, maxPersistentSize, collectionChecks);
        }

        protected void InitPool( int initial = 10, int maxPersistentSize = 20, bool collectionChecks = false)
        {
            _pool = new ObjectPool<T>(
                CreateNewPoolInstance,
                GetPoolInstance,
                ReleasePoolInstance,
                DestroyPoolInstance,
                collectionChecks,
                initial,
                maxPersistentSize);
        }

        protected abstract T CreateNewPoolInstance();
        protected abstract void GetPoolInstance(T poolObject);
        protected abstract void ReleasePoolInstance(T poolObject);
        protected abstract void DestroyPoolInstance(T poolObject);
    }
}