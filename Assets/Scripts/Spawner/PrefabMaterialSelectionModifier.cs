using CrawfisSoftware.PCG;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabMaterialSelectionModifier : IPrefabModifierAsync
    {
        private readonly IList<Material> materialList;
        private int index = 0;

        public PrefabMaterialSelectionModifier(IList<Material> materialList)
        {
            this.materialList = materialList;
        }

        public async Task ApplyAsync(GameObject prefab)
        {
            Renderer renderer = prefab.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = materialList[index++];
                index = (index >= materialList.Count) ? 0 : index;
            }
            await Task.CompletedTask;
        }
    }
}