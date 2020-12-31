using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabSelectorInstantiation : IPrefabSelectorAsync
    {
        private readonly GameObject prefab;

        public PrefabSelectorInstantiation(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public async Task<GameObject> CreateAsync(Vector3 position, Spawner spawner, int count)
        {
            GameObject newGameObject = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            //GameObject newGameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
            newGameObject.transform.localPosition = position;
            //return newGameObject;
            //await Task.CompletedTask;  return newGameObject;
            return await Task.FromResult<GameObject>(newGameObject);
        }
    }
}