using System.Threading.Tasks;
using UnityEngine.Pool;

namespace CrawfisSoftware
{
    /// <summary>
    /// Abstract base class for some asynchronous implementations of IPooler
    /// </summary>
    /// <typeparam name="T">The type of instances in the pool.</typeparam>
    public abstract class PoolerBaseAsync<T> : IPoolerAsync<T> where T : class
    {
        protected ObjectPool<T> _pool;

        public async Task<T> GetAsync()
        {
            T asset = await Task<T>.Run(() => { T result = _pool.Get(); return result; });
            return asset;
        }
        public async Task ReleaseAsync(T poolObject)
        {
            await Task.Run(() => { _pool.Release(poolObject); });
        }

        public PoolerBaseAsync(int initialSize = 100, int maxPersistentSize = 10000, bool collectionChecks = false)
        {
            InitPool(initialSize, maxPersistentSize, collectionChecks);
        }

        protected void InitPool(int initial = 10, int maxPersistentSize = 20, bool collectionChecks = false)
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

        protected abstract Task<T> CreateNewPoolInstanceAsync();
        protected abstract Task GetPoolInstanceAsync(T poolObject);
        protected abstract Task ReleasePoolInstanceAsync(T poolObject);
        protected abstract Task DestroyPoolInstanceAsync(T poolObject);

        private T CreateNewPoolInstance()
        {
            Task<T> task = CreateNewPoolInstanceAsync();
            task.Wait();
            return task.Result;
        }

        private void GetPoolInstance(T poolObject)
        {
            Task task = GetPoolInstanceAsync(poolObject);
            task.Wait();
        }

        private void ReleasePoolInstance(T poolObject)
        {
            Task task = ReleasePoolInstanceAsync(poolObject);
            task.Wait();
        }
        private void DestroyPoolInstance(T poolObject)
        {
            Task task = DestroyPoolInstanceAsync(poolObject);
            task.Wait();
        }
    }
}