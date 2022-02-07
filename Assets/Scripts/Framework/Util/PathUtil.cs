using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil
{
    //根目录
    public static readonly string AseetsPath = Application.dataPath;

    //需要打bundle的目录
    public static readonly string BuildResourcePath = AseetsPath + "/BuildResources/";

    //bundle 输出目录
    public static readonly string BundleOutPath = Application.streamingAssetsPath;


    ///<summary>
    /// 获取Unity的相对路径,把前面的路径剔除
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetUnityPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;

        return path.Substring(path.IndexOf("Assets"));
    }

    ///<summary>
    /// 获取有效路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetbundlePath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        return path.Trim().Replace("\\", "/");
    }

    public static string GetUIPath(string name)
    {
        return string.Format("Assets/BuildResources/UI/Prefabs/{0}.prefab", name);
    }
    public static string GetMusicPath(string name)
    {
        return string.Format("Assets/BuildResources/Audio/Music/{0}", name);
    }
    public static string GetSoundPath(string name)
    {
        return string.Format("Assets/BuildResources/Audio/Sound/{0}", name);
    }
    public static string GetEffectPath(string name)
    {
        return string.Format("Assets/BuildResources/Effect/Prefabs/{0}.prefab", name);
    }
    public static string GetSpritePath(string name)
    {
        return string.Format("Assets/BuildResources/Sprites/{0}", name);
    }
    public static string GetScenePath(string name)
    {
        return string.Format("Assets/BuildResources/Scene/{0}.unity", name);
    }
}

public class BundleInfo
{
    public string AssetsName;
    public string BundleName;
    public List<string> Dependences;
}
