using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class PrefabModifierBuilder : MonoBehaviour
    {
        protected IPrefabModifierAsync prefabModifier;

        public IPrefabModifierAsync PrefabModifier
        {
            get
            {
                return CreatePrefabModifier();
            }
        }

        protected virtual IPrefabModifierAsync CreatePrefabModifier()
        {
            return null;
        }
    }
}