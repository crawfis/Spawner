using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrawfisSoftware.Spawner;

namespace CrawfisSoftware.SpawnerTest
{
    public class TrickleSpawnControllerWithPause : MonoBehaviour
    {
        [SerializeField] private float timeBetweenTrickleSpawns = 1;
        [SerializeField] private float durationOfSpawns = 3;
        [SerializeField] private float delayBetweenSpawnRestarts = 2;
        [SerializeField] private Vector3 spawnLocation = Vector3.zero;
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject parentGO;

        private ISpawner spawner;

        void Start()
        {
            spawner = new SpawnerAndModifier(new PrefabGeneratorInstantiation(prefab), new List<IPrefabModifierAsync>());
            StartCoroutine(SpawnControlWithDelay());
        }

        private IEnumerator SpawnControlWithDelay()
        {
            while (true)
            {
                var trickleSpawn = StartCoroutine(SpawnControl());
                yield return new WaitForSeconds(durationOfSpawns);
                StopCoroutine(trickleSpawn);
                yield return new WaitForSeconds(delayBetweenSpawnRestarts);
            }
        }
        private IEnumerator SpawnControl()
        {
            while (true)
            {
                spawner.SpawnAsync(spawnLocation, parentGO.transform);
                yield return new WaitForSeconds(timeBetweenTrickleSpawns);
            }
        }
    }
}
