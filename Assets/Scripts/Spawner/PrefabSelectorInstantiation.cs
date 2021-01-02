﻿using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Creates new game objects using an existing game object as a template.
    /// </summary>
    public class PrefabSelectorInstantiation : IPrefabSelectorAsync
    {
        private readonly GameObject prefab;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefab">The game object to use as a template for new game objects.</param>
        public PrefabSelectorInstantiation(GameObject prefab)
        {
            this.prefab = prefab;
        }

        /// <inheritdoc/>
        public async Task<GameObject> CreateAsync(Vector3 position, Spawner spawner, int count)
        {
            GameObject newGameObject = UnityEditor.PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            //GameObject newGameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
            newGameObject.transform.localPosition = position;
            //return newGameObject;
            //await Task.CompletedTask;  return newGameObject;
            return await Task.FromResult<GameObject>(newGameObject);
        }
    }
}