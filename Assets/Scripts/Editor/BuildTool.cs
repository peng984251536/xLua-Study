using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD

public class BuildTool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
=======
using UnityEditor;
using System.IO;
using System.Linq;

public class BuildTool : Editor
{
    [MenuItem("Tools/Build Windows Bundle")]
    static void BuildWindowsBundle()
    {
        Build(BuildTarget.StandaloneWindows);
    }


    static void Build(BuildTarget buildTarget)
    {
        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();
        List<string> bundleInfos = new List<string>();

        string[] files = Directory.GetFiles(PathUtil.BuildResourcePath, "*", SearchOption.AllDirectories);//路径 - 匹配字符串 - 所有文件
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".meta"))
                continue;
            string filesName = PathUtil.GetbundlePath(files[i]);
            Debug.Log("file:" + filesName);
            AssetBundleBuild assetBundleBuild = new AssetBundleBuild();

            string assetName = PathUtil.GetUnityPath(filesName);
            assetBundleBuild.assetNames = new string[] { assetName };

            string bundleName = filesName.Replace(PathUtil.BuildResourcePath, "").ToLower();
            assetBundleBuild.assetBundleName = bundleName + AppConst.BundleExtension;
            assetBundleBuilds.Add(assetBundleBuild);

            //添加文件和依赖信息
            List<string> dependenceInfo = GetDependence(assetName);
            string bundleInfo = assetName + "|/" + bundleName + AppConst.BundleExtension;

            if (dependenceInfo.Count > 0)
                bundleInfo += "|" + string.Join("|", dependenceInfo);
            bundleInfos.Add(bundleInfo);

        }
        if (Directory.Exists(PathUtil.BundleOutPath))
            Directory.Delete(PathUtil.BundleOutPath, true);
        Directory.CreateDirectory(PathUtil.BundleOutPath);

        BuildPipeline.BuildAssetBundles(PathUtil.BundleOutPath, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, buildTarget);
        File.WriteAllLines(PathUtil.BundleOutPath + "/" + AppConst.FileListName, bundleInfos);
    }


    ///<summary>
    /// 获取Unity的相对路径,把前面的路径剔除
    /// </summary>
    /// <param name="curFile"></param>
    /// <returns></returns>
    static List<string> GetDependence(string curFile)
    {
        List<string> dependence = new List<string>();
        string[] files = AssetDatabase.GetDependencies(curFile);
        dependence = files.Where(file => !file.EndsWith(".cs") && !file.Equals(curFile)).ToList();
        return dependence;
    }

>>>>>>> BuildEditor
}
