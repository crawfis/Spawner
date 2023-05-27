using CrawfisSoftware.Spawner;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.SpawnerTest
{
    public class VoxelPillar : ISpawner
    {
        private readonly ISpawner _realTopSpawner;
        private readonly ISpawner _realBottomSpawner;
        private readonly float _skirtBaseHeight = 0;
        private readonly float _voxelHeight = 1f;

        public VoxelPillar(float voxelHeight, float skirtBaseHeight, ISpawner topSpawner, ISpawner bottomSpawner)
        {
            _voxelHeight = voxelHeight;
            _skirtBaseHeight = skirtBaseHeight;
            _realTopSpawner = topSpawner;
            _realBottomSpawner = bottomSpawner;
        }

        public Task<GameObject> SpawnAsync(Vector3 position, Transform parentTransform)
        {
            GameObject pillar = new GameObject("Pillar");
            pillar.transform.SetParent(parentTransform, false);
            var _ = _realTopSpawner.SpawnAsync(position, pillar.transform);
            float y = position.y - _voxelHeight;
            while(y > _skirtBaseHeight)
            {
                _ = _realBottomSpawner.SpawnAsync(new Vector3(position.x, y,position.z), pillar.transform);
                y -= _voxelHeight;
            }

            return Task.FromResult(pillar);
        }

        public IEnumerable<GameObject> SpawnStream(IEnumerable<Vector3> positionGenerator, Transform parentTransform)
        {
            foreach (var position in positionGenerator)
                yield return SpawnAsync(position, parentTransform).Result;
        }
    }
}