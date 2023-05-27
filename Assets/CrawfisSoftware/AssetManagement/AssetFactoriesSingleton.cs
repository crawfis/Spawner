using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GTMY.AssetManagement
{
    // Steps to use:
    // 1) Call Instance.RegisterFactory
    // 2) For each asset that this factory can provide, call Instance.RegisterAsset
    // 3) Use like a normal IAssetManagerAsync, but as a singleton.
    internal class AssetFactoriesSingleton : IAssetManagerAsync
    {
        private readonly Dictionary<string,IAssetManagerAsync> _assetFactories = new Dictionary<string, IAssetManagerAsync>();
        // List or Stack? Stack would allow a remapping. List allows a controlled pre-order and is easier to search.
        // Stack would allow me to push a null factory (or default factory) as the lowest priority. 
        private readonly Dictionary<string,List<string>> _assetToFactoriesMapping = new Dictionary<string, List<string>>();
        private readonly Dictionary<GameObject, IAssetManagerAsync> _allocatedAssets = new Dictionary<GameObject, IAssetManagerAsync>();

        public static AssetFactoriesSingleton Instance { get; private set; } = new AssetFactoriesSingleton();
        public Func<string, IList<string>,int> FactorySelection { get; set; } = (string assetName, IList<string> factorieNames) => { return factorieNames.Count-1; };
        public GameObject DefaultAsset { get; set; } = null; 

        private AssetFactoriesSingleton() {}

        // Note: There is not an easy way to Unregister a factory. Could add ActivateFactory / DeactivateFactory logic
        public void RegisterFactory(string factoryName, IAssetManagerAsync factory)
        {
            _assetFactories.Add(factoryName, factory);
        }

        public void RegisterAsset(string assetName, string factoryName)
        {
            if(_assetToFactoriesMapping.TryGetValue(assetName, out var factoryList))
                {
                factoryList.Add(factoryName);
            }
            else
            {
                factoryList = new List<string>();
                factoryList.Add(factoryName);
                _assetToFactoriesMapping.Add(assetName, factoryList);
            }
        }

        public IAssetManagerAsync GetFactory(string factoryName)
        {
            return _assetFactories[factoryName];
        }

        public async Task<GameObject> GetAssetAsync(string assetName)
        {
            var factory = SelectFactoryForAsset(assetName);
            GameObject asset = await factory.GetAssetAsync(assetName);
            _allocatedAssets.Add(asset, factory);
            return asset;
        }

        private IAssetManagerAsync SelectFactoryForAsset(string assetName)
        {
            // Todo: If we add Activate / deactivate then need to filter list to those that are active.
            if (_assetToFactoriesMapping.TryGetValue(assetName, out List<string> factoryList))
            {
                //var factoryList = _assetToFactoriesMapping[assetName];
                // Note: Will assume the rest is free of errors. Some of it is controlled in the class.
                int index = FactorySelection(assetName, factoryList);
                var factoryName = factoryList[index];
                var factory = _assetFactories[factoryName];
                return factory;
            }
            else
            {
                // Todo: will replace all error checking with a try / catch and return the DefaultAsset property.
                // Need a default factory, an IAssetManagerAsync that return this.DefaultAsset.
                throw new InvalidOperationException(string.Format("{0} is not registered with the AssetFactoriesSingleton",assetName));
            }
        }

        public async Task ReleaseAssetAsync(GameObject asset)
        {
            var factory = _allocatedAssets[asset];
            await factory.ReleaseAssetAsync(asset);
        }

        public async Task ReleaseAllAssetsAsync()
        {
            Task[] tasks = new Task[_assetFactories.Count];
            int index = 0;
            foreach(var factory in _assetFactories.Values)
            {
                tasks[index++] = factory.ReleaseAllAssetsAsync();
            }
            await Task.WhenAll(tasks);
        }
    }
}