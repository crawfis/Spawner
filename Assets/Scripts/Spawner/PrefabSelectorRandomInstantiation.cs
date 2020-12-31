using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabSelectorRandomInstantiation : IPrefabSelectorAsync
    {
        private readonly IList<GameObject> prefabList;
        private readonly System.Random random;

        public PrefabSelectorRandomInstantiation(IList<GameObject> prefabList, System.Random random = null)
        {
            this.prefabList = prefabList;
            this.random = random;
            if (random == null) this.random = new System.Random();
        }

        public async Task<GameObject> CreateAsync(Vector3 position, Spawner spawner, int count)
        {
            var prefab = prefabList[random.Next(prefabList.Count)];
            GameObject newGameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
            newGameObject.transform.localPosition = position;
            //return newGameObject;
            //await Task.CompletedTask;  return newGameObject;
            return await Task.FromResult<GameObject>(newGameObject);
        }
    }
}