using CrawfisSoftware.PCG;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Change a game object's material to the next on in a list of materials.
    /// </summary>
    public class PrefabMaterialSelectionModifier : IPrefabModifierAsync
    {
        private readonly IList<Material> materialList;
        private int index = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="materialList">A list of materials (not copied).</param>
        public PrefabMaterialSelectionModifier(IList<Material> materialList)
        {
            this.materialList = materialList;
        }

        /// <inheritdoc/>
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