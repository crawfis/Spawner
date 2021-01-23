using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Interface to perform the actual creation of new game objects.
    /// </summary>
    public interface IPrefabGeneratorAsync
    {
        /// <summary>
        /// Create a game object.
        /// </summary>
        /// <param name="position">The desired position of the new game object.</param>
        /// <param name="spawner">The spawner that is calling this method.</param>
        /// <param name="count">A counter from the spawner.</param>
        /// <returns>A game object.</returns>
        System.Threading.Tasks.Task<GameObject> CreateAsync(Vector3 position, Spawner spawner, int count);
    }
}