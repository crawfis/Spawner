using UnityEngine;

namespace GTMY.AssetManagement
{
    public class FixedParentingStrategy
    {
        private readonly GameObject parent;
        public FixedParentingStrategy(GameObject parent)
        {
            this.parent = parent;
        }
        public GameObject GetParent(Vector3 position, string key)
        {
            return parent;
        }
    }
}
