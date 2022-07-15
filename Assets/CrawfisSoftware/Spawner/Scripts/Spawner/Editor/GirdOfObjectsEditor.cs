using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CrawfisSoftware.SpawnerTest
{
    [CustomEditor(typeof(CrawfisSoftware.SpawnerTest.GridOfObjects))]
    public class GirdOfObjectsEditor : Editor
    {
        //[MenuItem("Window/PCG/Tile Creator")]
        //public static void ShowWindow()
        //{
        //    GetWindow<TileCreatorSpawner>(false, "Tile Creator", true);
        //}

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create"))
            {
                CreateTile();
            }
        }

        private void CreateTile()
        {
            var gridOfObjects = target as GridOfObjects;
            if (gridOfObjects != null)
            {
                Task task = gridOfObjects.CreateTileAsync();
                task.Wait();
            }
        }
    }
}
