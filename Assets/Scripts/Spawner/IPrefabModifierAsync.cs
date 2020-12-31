namespace CrawfisSoftware.Spawner
{
    public interface IPrefabModifierAsync
    {
        System.Threading.Tasks.Task ApplyAsync(UnityEngine.GameObject prefab);
    }
}
