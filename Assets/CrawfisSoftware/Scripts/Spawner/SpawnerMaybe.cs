using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Flexible Spawner of GameObjects using a predicate function. Instances are also IPrefabModifierAsync 
    /// </summary>
    public class SpawnerMaybe : SpawnerAndModifier
    {
        private readonly Func<Vector3, GameObject, bool> predicate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prefabSelector">Provides the new GameObjects when requested.</param>
        /// <param name="prefabModifiers">Used to perform any modifications after a game object is created.</param>
        /// <param name="predicate">A predicate function that determines at each position whether to return null or create a new GameObject.</param>
        public SpawnerMaybe(IPrefabGeneratorAsync prefabSelector, IList<IPrefabModifierAsync> prefabModifiers, Func<Vector3, GameObject, bool> predicate) 
            : base(prefabSelector, prefabModifiers)
        {
            this.predicate = predicate;
        }

        /// <summary>
        /// Create a new GameObject  or return null based on the predicate.
        /// </summary>
        /// <param name="position">The position that the new game object should be placed at.</param>
        /// <param name="parentTransform">A parent transform.</param>
        /// <returns>A newly created GameObject (wrapped in a Task).</returns>
        public override async System.Threading.Tasks.Task<GameObject> SpawnAsync(Vector3 position, Transform parentTransform)
        {
            if(predicate(position, parentTransform.gameObject))
            {
                return await base.SpawnAsync(position, parentTransform);
            }
            return await Task.FromResult<GameObject>(null);
        }
    }
}