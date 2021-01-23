using CrawfisSoftware.PointProvider;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class SpawnerBuilder : MonoBehaviour
    {
        //[SerializeField] private PositionProviderBuilder positionProviderBuilder;
        [SerializeField] private PrefabGeneratorBuilder prefabGeneratorBuilder;
        [SerializeField] private List<PrefabModifierBuilder> prefabModifierBuilders;
        [SerializeField] private Transform parent;

        private ISpawner spawner;

        public ISpawner Spawner { get
            {
                if (spawner == null)
                    CreateSpawner();
                return spawner;
            }
        }

        private void CreateSpawner()
        {
            //IPositionGenerator positionGenerator = positionProviderBuilder.PositionGenerator;
            IPrefabGeneratorAsync prefabGenerator = prefabGeneratorBuilder.PrefabGenerator;
            var modifiers = new List<IPrefabModifierAsync>();
            foreach(var builder in prefabModifierBuilders)
            {
                modifiers.Add(builder.PrefabModifier);
            }
            spawner = new Spawner(prefabGenerator, modifiers);
        }
    }
}
