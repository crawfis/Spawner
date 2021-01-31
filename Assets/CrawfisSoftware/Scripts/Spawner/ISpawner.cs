using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public interface ISpawner
    {
        Task<GameObject> SpawnAsync(Vector3 position, Transform parentTransform);
        IEnumerable<GameObject> SpawnStream(IEnumerable<Vector3> positionGenerator, Transform parentTransform);
    }
}