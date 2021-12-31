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
            spawner = new SpawnerAndModifier(new PrefabGeneratorPooled(prefab), new List<IPrefabModifierAsync>());
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