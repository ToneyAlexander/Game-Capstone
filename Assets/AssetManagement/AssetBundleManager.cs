using UnityEngine;
using System.Collections.Generic;

namespace CCC.AssetManagement
{
    public static class AssetBundleManager
    {
        private static IDictionary<string, AssetBundle> loadedAssetBundles = 
            new Dictionary<string, AssetBundle>();

        public static AssetBundle LoadAssetBundleAtPath(string path)
        {
            AssetBundle loadedAssetBundle = null;

            if (loadedAssetBundles.ContainsKey(path))
            {
                loadedAssetBundle = loadedAssetBundles[path];
            }
            else
            {
                var assetBundle = AssetBundle.LoadFromFile(path);

                if (assetBundle)
                {
                    loadedAssetBundles.Add(path, assetBundle);
                    loadedAssetBundle = assetBundle;
                }
                else
                {
                    Debug.LogError("[AssetBundleManager.LoadAssetBundleAtPath] " +
                        "failed to load asset bundle at '" + path + "'");
                }
            }

            return loadedAssetBundle;
        }

        public static void UnloadAssetBundleAtPath(string path)
        {
            if (loadedAssetBundles.ContainsKey(path))
            {
                loadedAssetBundles[path].Unload(false);
                loadedAssetBundles.Remove(path);
            }
        }
    }
}