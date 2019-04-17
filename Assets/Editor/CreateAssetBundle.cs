using UnityEditor;
using UnityEngine;
using System.IO;

public sealed class CreateAssetBundle
{
    private static readonly string path = 
        Path.Combine(Application.persistentDataPath, "AssetBundles");

    [MenuItem("Assets/Build AssetBundles/StandaloneWindows64")]
    static void BuildAllAssetBundlesWindows64()
    {
        CheckFolder();
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Assets/Build AssetBundles/StandaloneOSX")]
    static void BuildAllAssetBundlesOSX()
    {
        CheckFolder();
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
    }

    static void CheckFolder()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}