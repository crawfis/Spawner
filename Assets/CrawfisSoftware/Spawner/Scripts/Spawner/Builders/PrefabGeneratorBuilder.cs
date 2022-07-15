using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabGeneratorBuilder : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabs;
        [SerializeField] private bool randomize = true;

        private IPrefabGeneratorAsync prefabGenerator;

        public IPrefabGeneratorAsync PrefabGenerator
        {
            get
            {
                if (prefabGenerator == null)
                    CreatePrefabGenerator();
                return prefabGenerator;
            }
        }

        private void CreatePrefabGenerator()
        {
            prefabGenerator = null;
            if (prefabs.Count == 1)
            {
                prefabGenerator = new PrefabGeneratorInstantiation(prefabs[0]);
            }
            else if (randomize)
            {
                prefabGenerator = new PrefabGeneratorRandomInstantiation(prefabs);
            }
            else if(!randomize)
            {
                prefabGenerator = new PrefabGeneratorSequentialInstantiation(prefabs);
            }
        }
    }
}