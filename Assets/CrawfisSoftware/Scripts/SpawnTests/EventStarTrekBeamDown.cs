using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrawfisSoftware.Spawner;
using CrawfisSoftware.PointProvider;
using System;
using System.Linq;

namespace CrawfisSoftware.SpawnerTest
{
    public class EventStarTrekBeamDown : MonoBehaviour
    {
        [SerializeField] private float radius = 10;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parentGO;
        [SerializeField] private int polygonSides = 5;
        [SerializeField] private bool includeCenter = false;
        [SerializeField] private int minSpawnNumber = 1;
        [SerializeField] private int maxSpawnNumber = 5;

        private IEnumerator<IEnumerable<Vector3>> subsetEnumerator;
        private ISpawner spawner;
        private IEnumerable<Vector3> randomizedList;
        private readonly System.Random random = new System.Random();
        private int numberOfSpawnRequests = 0;

        void Start()
        {
            // Create Circle point generator
            // 1. Create points
            IEnumerable<Vector2> points2D = CreatePoints2D.RegularPolygon(polygonSides, Vector2.zero, radius, includeCenter);
            // 2. Add a y (height) value.
            IList<Vector3> points3D = Point2DTransforms.LiftXZto3D(points2D, 0).ToList();
            // 3. Randomize the list (note this is redone each time (i.e., each time GetEnumerator() is called)
            randomizedList = EnumerableHelpers.Shuffle(points3D, random);

            // Create Spawner
            spawner = new SpawnerAndModifier(new PrefabGeneratorInstantiation(prefab), new List<IPrefabModifierAsync>());
        }

        private int RandomSubSetSize(int iteration)
        {
            return random.Next(minSpawnNumber, maxSpawnNumber + 1);
        }

        public void SpawnNow()
        {
            numberOfSpawnRequests++;
            foreach (Vector3 point in randomizedList.Take(RandomSubSetSize(numberOfSpawnRequests)))
            {
                System.Threading.Tasks.Task<GameObject> task = spawner.SpawnAsync(point, parentGO);
                //task.Wait();
            }
        }
    }
}
