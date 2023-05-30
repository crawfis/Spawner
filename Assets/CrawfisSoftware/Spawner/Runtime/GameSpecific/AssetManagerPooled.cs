using CrawfisSoftware.Spawner;
using UnityEngine;

namespace CrawfisSoftware.GameSpecific
{
    internal static class AssetManagerPooled
    {
        // Todo: Add setter or determine better way to initialize.
        public static PrefabGeneratorPooled _poolerInstance;
        public static void ReleaseGameObject(GameObject asset)
        {
            _poolerInstance.Release(asset);
        }
    }
}