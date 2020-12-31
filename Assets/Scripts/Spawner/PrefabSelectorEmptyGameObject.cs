using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabSelectorEmptyGameObject : IPrefabSelectorAsync
    {
        public async Task<GameObject> CreateAsync(Vector3 position, Spawner spawner, int count)
        {
            var newGameObject = new GameObject();
            newGameObject.transform.localPosition = position;
            //return newGameObject;
            //await Task.CompletedTask;  return newGameObject;
            return await Task.FromResult<GameObject>(newGameObject);
        }
    }
}