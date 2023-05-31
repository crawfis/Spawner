using CrawfisSoftware.AssetManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    [CreateAssetMenu(fileName = "PrefabGenAdaptor", menuName = "CrawfisSoftware/AssetProviders/SpawnerAdaptor", order = 6)]
    public class PrefabGeneratorAdaptor : ScriptableAssetProviderBase<GameObject>, IPrefabGeneratorAsync
    {
        [SerializeField] private string _name = "anyItem";
        [SerializeField] private ScriptableAssetProviderBase<GameObject> _realAssetProvider;

        public async Task<GameObject> CreateAsync(Vector3 position, SpawnerAndModifier spawner, int counter)
        {
            var result = await GetAsync(_name);
            result.transform.localPosition = position;
            return result;
        }

        public override IEnumerable<string> AvailableAssets()
        {
            return _realAssetProvider.AvailableAssets();
        }

        public async override Task<GameObject> GetAsync(string name)
        {
            var result =await _realAssetProvider.GetAsync(name);
            return result;
        }

        public async override Task Initialize()
        {
            await _realAssetProvider.Initialize();
        }

        public override Task ReleaseAllAsync()
        {
            return _realAssetProvider.ReleaseAllAsync();
        }

        public override Task ReleaseAsync(GameObject instance)
        {
            return ReleaseAsync(instance);
        }
    }
}