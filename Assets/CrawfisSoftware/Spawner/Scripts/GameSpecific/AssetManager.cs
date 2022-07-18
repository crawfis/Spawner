using UnityEngine;

namespace CrawfisSoftware.GameSpecific
{
    internal static class AssetManager
    {
        public static void ReleaseGameObject(GameObject asset)
        {
            UnityEngine.Object.Destroy(asset);
        }
    }
}
