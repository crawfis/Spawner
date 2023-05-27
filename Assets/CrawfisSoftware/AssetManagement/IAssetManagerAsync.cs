using System.Threading.Tasks;

namespace GTMY.AssetManagement
{
    internal interface IAssetManagerAsync
    {
        Task<UnityEngine.GameObject> GetAssetAsync(string name);
        Task ReleaseAssetAsync(UnityEngine.GameObject asset);
        Task ReleaseAllAssetsAsync();
    }
}