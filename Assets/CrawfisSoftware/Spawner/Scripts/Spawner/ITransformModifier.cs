namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Interface to modify a transform.
    /// </summary>
    public interface ITransformModifier
    {
        /// <summary>
        /// Action to modify the given transform.
        /// </summary>
        /// <param name="currentTransform">The transform to modify.</param>
        void ModifyTransform(UnityEngine.Transform currentTransform);
    }
}
