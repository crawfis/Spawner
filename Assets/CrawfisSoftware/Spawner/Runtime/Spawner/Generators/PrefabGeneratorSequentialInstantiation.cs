using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Create a new game object from a list (in order).
    /// </summary>
    public class PrefabGeneratorSequentialInstantiation : IPrefabGeneratorAsync
    {
        private readonly IList<GameObject> prefabList;
        private int index = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefabList">A list of game objects to use as a template (not copied).</param>
        public PrefabGeneratorSequentialInstantiation(IList<GameObject> prefabList)
        {
            this.prefabList = prefabList;
        }

        /// <inheritdoc/>
        public async Task<GameObject> CreateAsync(Vector3 position, SpawnerAndModifier spawner, int counter)
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