using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public interface IPrefabSelectorAsync
    {
        System.Threading.Tasks.Task<GameObject> CreateAsync(Vector3 position, Spawner spawner, int count);
    }
}