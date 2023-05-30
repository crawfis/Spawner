using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Provide audio clips based on addressable _labels or keys.
    /// </summary>
    public class AddressableGeneratorLabels : IPrefabGeneratorAsync, System.IDisposable
    {
        private readonly List<string> _labels;
        private readonly System.Random _random;
        private readonly List<AsyncOperationHandle<GameObject>> _assetHandles = new List<AsyncOperationHandle<GameObject>>();
        private AsyncOperationHandle<IList<IResourceLocation>> _addressableAssets;
        private bool _disposedValue;
        private List<GameObject> _assetList = new List<GameObject>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="labels">A list of keywords which all must be present on the Addressable. For instance {"sfx","fire"}</param>
        public AddressableGeneratorLabels(IList<string> labels)
            : this(labels, new System.Random())
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="labels">A list of keywords which all must be present on the Addressable. For instance {"sfx","fire"}</param>
        /// <param name="randomGenerator">A System.Random instance.</param>
        public AddressableGeneratorLabels(IList<string> labels, System.Random randomGenerator)
        {
            this._labels = new List<string>(labels);
            this._random = randomGenerator;
        }

        /// <summary>
        /// Determines the clips and loads them into memory. This must be called first.
        /// </summary>
        public async void LoadAllAssetsAsync()
        {
            _addressableAssets = Addressables.LoadResourceLocationsAsync(_labels, Addressables.MergeMode.Intersection);
            _addressableAssets.Completed += AddressablesLoading_Completed;
            await _addressableAssets.Task;

            List<System.Threading.Tasks.Task> assetLoadingTasks = new List<System.Threading.Tasks.Task>(_assetHandles.Count);
            foreach (var handle in _assetHandles)
            {
                assetLoadingTasks.Add(handle.Task);
            }
            await System.Threading.Tasks.Task.WhenAll(assetLoadingTasks);
        }

        public Task<GameObject> CreateAsync(Vector3 position, SpawnerAndModifier spawner, int counter)
        {
            throw new System.NotImplementedException();
        }

        private void AddressablesLoading_Completed(AsyncOperationHandle<IList<IResourceLocation>> addressHandles)
        {
            if (addressHandles.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var resourceLocation in addressHandles.Result)
                {
                    var addressHandle = Addressables.LoadAssetAsync<GameObject>(resourceLocation);
                    // Keep track of the handles so we can clean up.
                    _assetHandles.Add(addressHandle);
                    addressHandle.Completed += handle =>
                    {
                        GameObject asset = handle.Result;
                        if (asset != null) _assetList.Add(asset);
                    };

                }
            }
            // I do not think I need the AsyncOperationHandle anymore
            Addressables.Release(addressHandles);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    foreach (var handle in this._assetHandles)
                    {
                        Addressables.Release<GameObject>(handle);
                    }
                    Addressables.Release(_addressableAssets);
                    _assetList.Clear();
                }

                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AudioClipProvider()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void System.IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}