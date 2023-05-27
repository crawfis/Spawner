using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace GTMY.AssetManagement
{
    public class AssetCreationFactory
    {
        //private readonly string[] addressableLabels;
        private readonly IEnumerable<string> labels;
        private readonly System.Random randomGenerator;

        public IList<IResourceLocation> AddressableAssets { get; private set; }
        public Func<Vector3, string, GameObject> ParentStrategy { get; set; }

        public AssetCreationFactory(IEnumerable<string> labels, System.Random randomGenerator, GameObject parent = null)
        {
            this.labels = labels.ToList<string>();
            //addressableLabels = labels.ToArray();
            this.randomGenerator = randomGenerator;
            this.ParentStrategy = new FixedParentingStrategy(new GameObject(parent.name)).GetParent;
        }

        public async Task LoadAddresses()
        {
            AddressableAssets = await Addressables.LoadResourceLocationsAsync(labels, Addressables.MergeMode.Intersection).Task;
            Debug.Log(AddressableAssets.Count);
        }
        public async Task InstantiateAnyValidAddressable(IList<Vector3> positions, Action<GameObject,int> completedCallback = null)
        {
            int count = AddressableAssets.Count;
            if (count == 0)
                return;
            List<Quaternion> rotations = new List<Quaternion>(1);
            rotations.Add(UnityEngine.Quaternion.identity);
            await InstantiateAnyValidAddressable(positions, rotations, completedCallback);
        }

        public async Task InstantiateAnyValidAddressable(IList<Vector3> positions, IList<Quaternion> rotations, Action<GameObject,int> completedCallback = null)
        {
            int count = AddressableAssets.Count;
            if (count == 0)
            {   
                return;
            }
            // Todo: This is dangerous. It will be changed between calls and before used by the wrapping callback.
            // Perhaps fixed by an anonymous delegate, but not sure it will remember the state.
            var tasks = new List<Task>();
            int rotationIndex = 0;
            int prefabIndex = 0;
            foreach (var location in positions)
            {
                IResourceLocation prefab = AddressableAssets[randomGenerator.Next(count)];
                Transform parent = ParentStrategy(location, null).transform;
                var assetHandle = Addressables.InstantiateAsync(prefab, location, rotations[rotationIndex++], parent);
                if (rotationIndex >= rotations.Count) rotationIndex = 0;
                if (completedCallback != null)
                {
                    assetHandle.Completed += obj =>
                    {
                        GameObject createdAsset = obj.Result;
                        completedCallback(createdAsset, prefabIndex++);
                    };
                }
                tasks.Add(assetHandle.Task);
            }
            await Task.WhenAll(tasks);
            tasks.Clear();
        }
    }
}
