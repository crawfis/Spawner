using System.Collections.Generic;

namespace GTMY.AssetManagement
{
    internal static class AssetManagerStack
    {
        private static readonly Stack<IAssetManagerAsync> _managerStack = new Stack<IAssetManagerAsync>();
        public static IAssetManagerAsync Instance { get { return _managerStack.Peek(); }  }

        public static void PushInstance(IAssetManagerAsync assetManager)
        {
            _managerStack.Push(assetManager);
        }

        public static IAssetManagerAsync PopInstance()
        {
            return _managerStack.Count > 0 ? _managerStack.Pop() : null;
        }
    }
}
