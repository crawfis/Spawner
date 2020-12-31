using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabSelectorSequentialInstantiation : IPrefabSelectorAsync
    {
        private readonly IList<GameObject> prefabList;
        private int index = 0;

        public PrefabSelectorSequentialInstantiation(IList<GameObject> prefabList)
        {
            this.prefabList = prefabList;
        }

        public async Task<GameObject> CreateAsync(Vector3 position, Spawner spawner, int count)
        {
            var prefab = prefabList[index++];
            if (index >= prefabList.Count) index = 0;
            GameObject newGameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
            newGameObject.transform.localPosition = position;
            //return newGameObject;
            //await Task.CompletedTask;  return newGameObject;
            return await Task.FromResult<GameObject>(newGameObject);
        }
    }
}