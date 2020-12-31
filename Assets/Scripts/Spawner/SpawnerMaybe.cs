using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class SpawnerMaybe : Spawner
    {
        private readonly Func<Vector3, GameObject, bool> predicate;

        public SpawnerMaybe(IPrefabSelectorAsync prefabSelector, IList<IPrefabModifierAsync> prefabModifiers, Func<Vector3, GameObject, bool> predicate) 
            : base(prefabSelector, prefabModifiers)
        {
            this.predicate = predicate;
        }

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