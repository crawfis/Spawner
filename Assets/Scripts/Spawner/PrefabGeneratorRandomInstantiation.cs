using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Create a new game object using a template randomly from a list of templates.
    /// </summary>
    public class PrefabGeneratorRandomInstantiation : IPrefabGeneratorAsync
    {
        private readonly IList<GameObject> prefabList;
        private readonly System.Random random;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefabList">A list of prefab templates.</param>
        /// <param name="random">(optional) A System.Random random number generator.</param>
        public PrefabGeneratorRandomInstantiation(IList<GameObject> prefabList, System.Random random = null)
        {
            this.prefabList = prefabList;
            this.random = random;
            if (random == null) this.random = new System.Random();
        }

        /// <inheritdoc/>
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