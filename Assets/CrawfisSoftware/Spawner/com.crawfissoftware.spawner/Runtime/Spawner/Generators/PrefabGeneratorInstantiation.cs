using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Creates new game objects using an existing game object as a template.
    /// </summary>
    public class PrefabGeneratorInstantiation : IPrefabGeneratorAsync
    {
        private readonly GameObject prefab;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefab">The game object to use as a template for new game objects.</param>
        public PrefabGeneratorInstantiation(GameObject prefab)
        {
            this.prefab = prefab;
        }

        /// <inheritdoc/>
        public async Task<GameObject> CreateAsync(Vector3 position, SpawnerAndModifier spawner, int counter)
        {
#if UNITY_EDITOR
            GameObject newGameObject = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
#else
            GameObject newGameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
#endif
            newGameObject.transform.localPosition = position;
            //return newGameObject;
            //await Task.CompletedTask;  return newGameObject;
            return await Task.FromResult<GameObject>(newGameObject);
        }
    }
}