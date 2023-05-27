using CrawfisSoftware.PointProvider;
using CrawfisSoftware.Spawner;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrawfisSoftware.SpawnerTest
{
    public class GridOfObjects : MonoBehaviour
    {
        [SerializeField] [Range(2, 256)] private int numberAlongX = 10;
        [SerializeField] [Range(2, 256)] private int numberAlongZ = 10;
        [SerializeField] private GameObject[] prefabs;
        [Range(1,10)]
        [SerializeField] private int numberOfStacks = 5;
        [SerializeField] private Bounds bounds;
        [SerializeField] private Vector2 padding;
        [SerializeField] private string tileName = "TileOfObjects_0000";
        [SerializeField] private Vector3 maxJitter;
        [SerializeField] private Vector3 minRotation;
        [SerializeField] private Vector3 maxRotation;
        [SerializeField] private Vector3 minScale;
        [SerializeField] private Vector3 maxScale;
        [SerializeField] private Color minColorShift;
        [SerializeField] private Color maxColorShift;
        [SerializeField] private Vector2 tileSizeOutputOnly;
        [SerializeField] private List<Material> materialList;

        private System.Random random = new System.Random();
        private GameObject tileBase;
        private Vector2 tileCenter;

        private async System.Threading.Tasks.Task Start()
        {
            // Todo: Switch to Addressables??? (tile of assetReferences, tile of _labels, or done at editor time?)
            // Todo: Move to an Editor Script
            // Todo: Move from a grid to a list of spawnLocations that can be filled with a grid.
            // Todo: Add code to save hierarchy as a tile prefab.
            tileCenter = 0.7f * new Vector2((float)numberAlongX / 2f, (float)numberAlongZ/2f);
            await CreateTileAsync();
            UnityEditor.PrefabUtility.SaveAsPrefabAsset(tileBase, @"Assets\Prefabs\Tile.prefab", out bool success);
            Debug.Log(success.ToString());
        }

        public async System.Threading.Tasks.Task CreateTileAsync()
        {
            //Bounds bounds = prefab.GetComponent<MeshRenderer>().bounds;
            tileSizeOutputOnly.x = numberAlongX * (bounds.size.x + padding.x);
            tileSizeOutputOnly.y = numberAlongZ * (bounds.size.z + padding.y);

            var prefabTransformModifier = new PrefabTransformModifierComposite();
            var jitter = new TransformPositionJitter(-maxJitter, maxJitter);
            prefabTransformModifier.AddTransformModifier(jitter);
            var rotator = new TransformRotationJitter(minRotation, maxRotation);
            prefabTransformModifier.AddTransformModifier(rotator);
            var scaler = new TransformRandomScaleWithinRange(minScale, maxScale);
            prefabTransformModifier.AddTransformModifier(scaler);

            var prefabModifiers = new List<IPrefabModifierAsync>();
            //var colorModifier = new PrefabColorModifier(minColorShift, maxColorShift);
            var colorModifier = new PrefabMaterialSelectionModifier(materialList);
            prefabModifiers.Add(colorModifier);
            var secondGridOffset = new PrefabTransformModifier(new Vector3(0.0f, 0.65f, 0.0f), Vector3.zero, Vector3.one);
            var secondPrefabTransformModifier = new PrefabTransformModifierComposite();
            secondPrefabTransformModifier.AddTransformModifier(secondGridOffset);
            secondPrefabTransformModifier.AddTransformModifier(jitter);
            secondPrefabTransformModifier.AddTransformModifier(rotator);
            secondPrefabTransformModifier.AddTransformModifier(scaler);
            var secondPrefabModifiers = new List<IPrefabModifierAsync>();
            var secondColorModifier = new PrefabColorModifier(minColorShift, maxColorShift, true);
            secondPrefabModifiers.Add(secondColorModifier);
            secondPrefabModifiers.Add(secondPrefabTransformModifier);

            //var secondPrefabSpawner = new SpawnerMaybe(new PrefabGeneratorInstantiation(prefabs[1]), secondPrefabModifiers, MiddleOnly);

            //var prefabSpawner = new SpawnerAndModifier(new PrefabGeneratorSequentialInstantiation(prefabs), new List<IPrefabModifierAsync>() { prefabTransformModifier, colorModifier, secondPrefabSpawner });
            var pooler = new PrefabGeneratorPooled(new PrefabGeneratorRandomInstantiation(prefabs), 1000);
            GameSpecific.AssetManagerPooled._poolerInstance = pooler;
            var prefabSpawner = new SpawnerAndModifier(pooler, new List<IPrefabModifierAsync>() { prefabTransformModifier, colorModifier });
            //var prefabSpawner = new SpawnerAndModifier(new PrefabGeneratorRandomInstantiation(prefabs), new List<IPrefabModifierAsync>() { prefabTransformModifier, colorModifier });
            //var prefabSpawner = new Spawner(new PrefabSelectorInstantiation(prefabs[0]), new List<IPrefabModifierAsync>() { prefabTransformModifier, colorModifier, secondPrefabSpawner });
            var rootObjectModifiers = new List<IPrefabModifierAsync>() { prefabSpawner };
            var emptySpawner = new SpawnerAndModifier(new PrefabGeneratorEmptyGameObject(), rootObjectModifiers);

            tileBase = new GameObject(tileName);
            //var positions = DeterminePositions();
            //var positions = new PointProviderGrid(numberAlongX, numberAlongZ, bounds.size, bounds.center);
            var enumerable2 = CreatePoints2D.Grid(numberAlongX, numberAlongZ, new Vector2(bounds.size.x, bounds.size.z), new Vector2(bounds.center.x, bounds.center.z));
            //var enumerable2 = Point2DTransforms.Jitter(enumerable, new Vector2(0.1f, 0.1f));
            //var discSampler = new PoissonDiscSampler(tileSizeOutputOnly.x, tileSizeOutputOnly.y, bounds.size.x + padding.x);
            //var enumerable2 = discSampler.Samples();
            //var enumerable3 = Point2DTransforms.MirrorHorizontal(enumerable2);
            //var enumerable4 = Point2DTransforms.MirrorVertical(enumerable3);
            //var enumerable5 = EnumerableHelpers.Shuffle(enumerable4.ToList());
            var enumerable5 = CreatePoints2D.MaskedEnumeration(enumerable2, CarveOutCenter);
            //var enumerable6 = Point2DTransforms.LiftXZto3D(enumerable5, 0);
            var enumerable7 = StackedObjectsPointProvider.DuplicateInY(enumerable5, StackHeight, 0, 0.65f);
            //var enumerable7 = StackedObjectsPointProvider.DuplicateInY(enumerable2, (i) =>
            //{ return (int)((float)(i % numberAlongZ) * (float)numberOfStacks / (float)(numberAlongX - 1)); },
            //{ return (int)((i % numberAlongZ)); },
            //{ return numberOfStacks; },
            //0, bounds.extents.y);
            var poolSpawner = new PooledSpawner(emptySpawner);
            foreach (var gameObject in emptySpawner.SpawnStream(enumerable7, tileBase.transform))
            {
                ;
            }
            await System.Threading.Tasks.Task.CompletedTask;
        }

        private bool CarveOutCenter(Vector2 position)
        {
            float distance = Vector2.SqrMagnitude(tileCenter - position);
            if (distance < 6f) return false;
            return true;
        }

        private int StackHeight(int index)
        {
            return numberOfStacks;
            int i = index % numberAlongZ;
            if (i < 3 || i > numberAlongZ - 2)
                return numberOfStacks;
            return (index % 2);
        }
        private bool MiddleOnly(Vector3 position, GameObject parent)
        {
            const float border = 1;
            float x = parent.transform.parent.transform.localPosition.x;
            float z = parent.transform.parent.transform.localPosition.z;
            //if ((x > border) && x < (tileSizeOutputOnly.x-border))
            {
                //if ((z > border) && random.Next(100) > 70)
                if (random.Next(100) > 70)
                {
                    return true;
                }
            }
            return false;
        }

        private IEnumerable<Vector3> DeterminePositions()
        {
            Vector3 size = bounds.size;
            Vector3 origin = bounds.center;
            Vector3 position = new Vector3(-origin.x, 0, -origin.z); // Recenter to (0,y,0)

            position.x += size.x / 2;
            // Create Parent
            for (int i = 0; i < numberAlongX; i++)
            {
                position.z = -origin.z + size.z / 2;
                for (int j = 0; j < numberAlongZ; j++)
                {
                    yield return position;
                    //var objectBase = await emptySpawner.SpawnAsync(position, tileBase.transform);
                    position.z += size.z + padding.y;
                }
                position.x += size.x + padding.x;
            }
        }
    }
}