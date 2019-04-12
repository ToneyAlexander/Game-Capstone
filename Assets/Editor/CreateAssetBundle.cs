using UnityEditor;

public sealed class CreateAssetBundle
{
    [MenuItem("Assets/Build AssetBundles/StandaloneWindows64")]
    static void BuildAllAssetBundlesWindows64()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Assets/Build AssetBundles/StandaloneOSX")]
    static void BuildAllAssetBundlesOSX()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
    }
}