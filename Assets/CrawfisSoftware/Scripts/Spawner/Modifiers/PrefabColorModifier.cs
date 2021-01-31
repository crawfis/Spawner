using CrawfisSoftware.PCG;
using System.Threading.Tasks;
using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    /// <summary>
    /// Modify the material color with a color within a random range (using RGBA).
    /// </summary>
    public class PrefabColorModifier : IPrefabModifierAsync
    {
        private readonly RandomColorWithinRange randomColor;
        private readonly bool modifyChildren;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="color1">A color for one of the bounds.</param>
        /// <param name="color2">Color for the other bounds.</param>
        /// <param name="modifyChildren">Modify all children's renderers if true.</param>
        /// <remarks>Only use during runtime. Will not work in editor.</remarks>
        public PrefabColorModifier(Color color1, Color color2, bool modifyChildren = false)
        {
            randomColor = new RandomColorWithinRange(color1, color2);
            this.modifyChildren = modifyChildren;
        }

        /// <inheritdoc/>
        public async Task ApplyAsync(GameObject prefab)
        {
            if(modifyChildren)
            {
                ModifyAllColors(prefab);
            }
            else
            {
                ModifyColor(prefab);
            }
            await Task.CompletedTask;
        }

        private void ModifyColor(GameObject prefab)
        {
            Renderer renderer = prefab.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color newColor = randomColor.GetNext();
                // Option 1: Editor gives error - use sharedMaterial. Prefab materials are broken when dragged into folder.
                //renderer.material.color = newColor;
                // Option 2: All prefabs have the same color.
                //renderer.sharedMaterial.color = newColor;
                // Option 3: Prefab materials are broken when dragged into folder. New materials are lost.
                var newMaterial = new Material(renderer.sharedMaterial);
                newMaterial.SetColor("_Color", newColor);
                renderer.material = newMaterial;
            }
        }

        private void ModifyAllColors(GameObject prefab)
        {
            var renderers = prefab.GetComponentsInChildren<MeshRenderer>();
            foreach (var renderer in renderers)
            {
                Color newColor = randomColor.GetNext();
                // Option 1: Editor gives error - use sharedMaterial. Prefab materials are broken when dragged into folder.
                //renderer.material.color = newColor;
                // Option 2: All prefabs have the same color.
                //renderer.sharedMaterial.color = newColor;
                // Option 3: Prefab materials are broken when dragged into folder. New materials are lost.
                var newMaterial = new Material(renderer.sharedMaterial);
                newMaterial.SetColor("_Color", newColor);
                renderer.material = newMaterial;
            }
        }
    }
}
