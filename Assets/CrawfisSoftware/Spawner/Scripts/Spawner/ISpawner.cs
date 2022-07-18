using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Interface for creating (spawning) new heirachies or objects.
    /// </summary>
    public interface ISpawner
    {
        /// <summary>
        /// Spawn a GameObject at a specified position and set its parent.
        /// </summary>
        /// <param name="position">The local position to place the spawned object.</param>
        /// <param name="parentTransform">The Transform to attach this object as childrent.</param>
        /// <returns>The resulting game object, or task. Use var go = await SpawnAsync.</returns>
        Task<GameObject> SpawnAsync(Vector3 position, Transform parentTransform);
        /// <summary>
        /// Spawn GameObjects at the specified list of position and set their parent
        /// </summary>
        /// <param name="positionGenerator">A list of positions for each new game object.</param>
        /// <param name="parentTransform">The Transform to attach any new game objects as children.</param>
        /// <returns>An IEnumerable of spawned object useful for a foreach loop.</returns>
        IEnumerable<GameObject> SpawnStream(IEnumerable<Vector3> positionGenerator, Transform parentTransform);
    }
}