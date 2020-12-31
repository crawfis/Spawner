using CrawfisSoftware.Spawner;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridOfObjects : MonoBehaviour
{
    [Range(2, 256)]
    public int numberAlongX = 10;
    [Range(2, 256)]
    public int numberAlongZ = 10;
    [SerializeField]
    private GameObject[] prefabs;
    public Bounds bounds;
    public Vector2 padding;
    public string tileName = "TileOfObjects_0000";
    public Vector3 maxJitter;
    public Vector3 minRotation;
    public Vector3 maxRotation;
    public Vector3 minScale;
    public Vector3 maxScale;
    public Color minColorShift;
    public Color maxColorShift;
    public Vector2 tileSizeOutputOnly;
    private System.Random random = new System.Random();
    private GameObject tileBase;
    [SerializeField]
    private List<Material> materialList;

    private async System.Threading.Tasks.Task Start()
    {
        // Todo: Switch to Addressables??? (tile of assetReferences, tile of labels, or done at editor time?)
        // Todo: Move to an Editor Script
        // Todo: Move from a grid to a list of spawnLocations that can be filled with a grid.
        // Todo: Add code to save hierarchy as a tile prefab.
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
        var scaler = new TransformScaleJitter(minScale, maxScale);
        prefabTransformModifier.AddTransformModifier(scaler);

        var prefabModifiers = new List<IPrefabModifierAsync>();
        //var colorModifier = new PrefabColorModifier(minColorShift, maxColorShift);
        var colorModifier = new PrefabMaterialSelectionModifier(materialList);
        prefabModifiers.Add(colorModifier);
        var secondGridOffset = new PrefabTransformModifier(new Vector3(0.0f, 1.05f, 0.0f), Vector3.zero, Vector3.one);
        var secondPrefabTransformModifier = new PrefabTransformModifierComposite();
        secondPrefabTransformModifier.AddTransformModifier(secondGridOffset);
        //secondPrefabTransformModifier.AddTransformModifier(jitter);
        secondPrefabTransformModifier.AddTransformModifier(rotator);
        secondPrefabTransformModifier.AddTransformModifier(scaler);
        var secondPrefabModifiers = new List<IPrefabModifierAsync>();
        var secondColorModifier = new PrefabColorModifier(minColorShift, maxColorShift);
        secondPrefabModifiers.Add(secondColorModifier);
        secondPrefabModifiers.Add(secondPrefabTransformModifier);

        var secondPrefabSpawner = new SpawnerMaybe(new PrefabSelectorInstantiation(prefabs[1]), secondPrefabModifiers, MiddleOnly);

        var prefabSpawner = new Spawner(new PrefabSelectorSequentialInstantiation(prefabs), new List<IPrefabModifierAsync>() { prefabTransformModifier, colorModifier, secondPrefabSpawner });
        var rootObjectModifiers = new List<IPrefabModifierAsync>() { prefabSpawner };
        var emptySpawner = new Spawner(new PrefabSelectorEmptyGameObject(), rootObjectModifiers);

        tileBase = new GameObject(tileName);
        var positions = DeterminePositions();
        foreach(var gameObject in emptySpawner.SpawnStream(positions, tileBase.transform))
        {
            ;
        }
        await System.Threading.Tasks.Task.CompletedTask;
    }

    private bool MiddleOnly(Vector3 position, GameObject parent)
    {
        const float border = 1;
        float x = parent.transform.parent.transform.localPosition.x;
        float z = parent.transform.parent.transform.localPosition.z;
        if ((x > border) && x < (tileSizeOutputOnly.x-border))
        {
            if((z > border) && random.Next(100) > 70)
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
