using CrawfisSoftware.PointProvider;
using CrawfisSoftware.Spawner;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.SpawnerTest
{
    internal class VoxelGrid : MonoBehaviour
    {
        [SerializeField][Range(2, 256)] private int numberAlongX = 10;
        [SerializeField][Range(2, 256)] private int numberAlongZ = 10;
        [SerializeField] private GameObject[] _topPrefabs;
        [SerializeField] private GameObject[] _bottomPrefabs;
        [Range(1, 10)]
        [SerializeField] private int numberOfStacks = 5;
        [SerializeField] private Bounds bounds;
        [SerializeField] private string tileName = "VoxelGrid";

        private async System.Threading.Tasks.Task Start()
        {
            await CreateTileAsync();
        }

        public async System.Threading.Tasks.Task CreateTileAsync()
        {
            var prefabTransformModifier = new PrefabTransformModifierComposite();
            var pooler = new PrefabGeneratorPooled(new PrefabGeneratorRandomInstantiation(_bottomPrefabs), 100);
            GameSpecific.AssetManagerPooled._poolerInstance = pooler;
            var bottomPrefabSpawner = new SpawnerAndModifier(pooler, new List<IPrefabModifierAsync>() { });

            var tileBase = new GameObject(tileName + "Bottom");
            var enumerable2 = CreatePoints2D.Grid(numberAlongX, numberAlongZ, new Vector2(bounds.size.x, bounds.size.z), new Vector2(bounds.center.x, bounds.center.z));
            var enumerableTop = Point2DTransforms.LiftXZto3D(enumerable2, 0);
            var enumerable7 = StackedObjectsPointProvider.DuplicateInY(enumerable2, StackHeight, 0, bounds.size.y);
            //foreach (var gameObject in bottomPrefabSpawner.SpawnStream(enumerable7, tileBase.transform))
            //{
            //    ;
            //}
            var topTileBase = new GameObject(tileName + "Top");
            var topPrefabTransformModifier = new PrefabTransformModifierComposite();
            var topAssetProvider = new PrefabGeneratorSequentialInstantiation(_topPrefabs);
            var topPrefabSpawner = new SpawnerAndModifier(topAssetProvider, new List<IPrefabModifierAsync>() { });
            var voxelPillarSpawner = new VoxelPillar(1, 0, topPrefabSpawner, bottomPrefabSpawner);

            int index = 0;
            foreach (var position2D in enumerable2)
            {
                float y = StackHeight(index++);
                Vector3 position = new Vector3(position2D.x, y, position2D.y);
                //var _ = topPrefabSpawner.SpawnAsync(position, topTileBase.transform);
                var _ = voxelPillarSpawner.SpawnAsync(position, topTileBase.transform);
            }
            await System.Threading.Tasks.Task.CompletedTask;
        }

        private int StackHeight(int index)
        {
            int i = index % numberAlongZ;
            int j = index / numberAlongX;
            float x = i * 0.7f;
            float y = j * 0.31f;
            float height = numberOfStacks + 1.6f * ((float)Math.Cos(x) * (float)Math.Sin(y) + 2f);
            return (int)height;
        }
    }
}
