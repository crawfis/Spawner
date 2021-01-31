using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Creates a new Unity Empty GameObject.
    /// </summary>
    public class PrefabGeneratorEmptyGameObject : IPrefabGeneratorAsync
    {
        /// <inheritdoc/>
        public async Task<GameObject> CreateAsync(Vector3 position, SpawnerAndModifier spawner, int count)
        {
            var newGameObject = new GameObject();
            newGameObject.transform.localPosition = position;
            //return newGameObject;
            //await Task.CompletedTask;  return newGameObject;
            return await Task.FromResult<GameObject>(newGameObject);
        }
    }
}