using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrawfisSoftware.Spawner;

namespace CrawfisSoftware.SpawnerTest
{
    public class TrickleSpawnerPooledExample : MonoBehaviour
    {
        [SerializeField] private float timeBetweenSpawns = 1;
        [SerializeField] private Vector3 spawnLocation = Vector3.zero;
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject parentGO;

        private ISpawner spawner;

        void Start()
        {
            var pooler = new PrefabGeneratorPooled(new PrefabGeneratorInstantiation(prefab),1000);
            GameSpecific.AssetManagerPooled._poolerInstance = pooler;
            spawner = new SpawnerAndModifier(pooler, new List<IPrefabModifierAsync>());
            StartCoroutine(SpawnControl());
        }

        private IEnumerator SpawnControl()
        {
            while (true)
            {
                spawner.SpawnAsync(spawnLocation, parentGO.transform);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }
}