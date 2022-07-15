namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Interface to modify a newly instantiated game object.
    /// </summary>
    public interface IPrefabModifierAsync
    {
        /// <summary>
        /// Asynchronously perform the desired action.
        /// </summary>
        /// <param name="prefab">A game object.</param>
        /// <returns>Void (Task for asynchronous programming).</returns>
        System.Threading.Tasks.Task ApplyAsync(UnityEngine.GameObject prefab);
    }
}
