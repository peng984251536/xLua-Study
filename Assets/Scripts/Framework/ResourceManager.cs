using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UObject = UnityEngine.Object;


public class ResourceManager : MonoBehaviour
{
    private Dictionary<string, BundleInfo> bundleInfos = new Dictionary<string, BundleInfo>();



    private void Start()
    {
        if (AppConst.GameMode != GameMode.EditorMode)
            ParseVersionFile();

        Invoke("Test", 3.0f);
    }

    private void Test()
    {
        LoadUI("Button", OnComplete);
    }
    public void OnComplete(UObject uObject)
    {
        GameObject go = Instantiate(uObject) as GameObject;
        go.transform.SetParent(this.transform);
        go.SetActive(true);
        go.transform.localPosition = Vector3.zero;
    }

    ///<summary>
    /// 获取Unity的相对路径,把前面的路径剔除
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private void ParseVersionFile()
    {
        bundleInfos.Clear();

        //版本文件路径
        string url = Path.Combine(PathUtil.BundleOutPath, AppConst.FileListName);
        string[] data = File.ReadAllLines(url);

        //解析文件信息
        for (int i = 0; i < data.Length; i++)
        {
            BundleInfo bundleInfo = new BundleInfo();
            string[] info = data[i].Split('|');
            bundleInfo.AssetsName = info[0];
            bundleInfo.BundleName = info[1];

            bundleInfo.Dependences = new List<string>(info.Length - 2);
            for (int j = 2; j < info.Length; j++)
            {
                bundleInfo.Dependences.Add(info[j]);
            }
            bundleInfos.Add(info[0], bundleInfo);
        }
    }


    ///<summary>
    /// 异步加载本地bundle资源
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    IEnumerator LoadBundleAsync(string assetName, Action<UObject> action = null)
    {
        string bundleName = bundleInfos[assetName].BundleName;
        string bundlePath = Path.Combine(PathUtil.BundleOutPath, bundleName);

        foreach (var _assetName in bundleInfos[assetName].Dependences)
        {
            yield return LoadBundleAsync(_assetName);
        }

        //异步加载出本地的包
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(PathUtil.BundleOutPath + bundleName);
        yield return request;

        //异步地从包中加载name的asste
        AssetBundleRequest bundleRequest = request.assetBundle.LoadAssetAsync(assetName);
        yield return bundleRequest;

        action?.Invoke(bundleRequest?.asset);
    }


    ///<summary>
    /// 编辑器环境加载资源
    /// </summary>
    /// <param name="assetName"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private void EditorLoadAsset(string assetName, Action<UObject> action)
    {
        UObject obj = UnityEditor.AssetDatabase.LoadAssetAtPath(assetName, typeof(UObject));

        action?.Invoke(obj);
    }

    private void LoadAsset(string assetName, Action<UObject> action)
    {
        if (AppConst.GameMode == GameMode.EditorMode)
            EditorLoadAsset(assetName, action);
        else
            StartCoroutine(LoadBundleAsync(assetName, action));
    }

    //Tap:卸载暂时不做

    public void LoadUI(string name, Action<UObject> action)
    {
        LoadAsset(PathUtil.GetUIPath(name), action);
    }
    public void LoadSprite(string name, Action<UObject> action)
    {
        LoadAsset(PathUtil.GetSpritePath(name), action);
    }
    public void LoadSound(string name, Action<UObject> action)
    {
        LoadAsset(PathUtil.GetSoundPath(name), action);
    }
    public void LoadMusic(string name, Action<UObject> action)
    {
        LoadAsset(PathUtil.GetMusicPath(name), action);
    }
    public void LoadEffect(string name, Action<UObject> action)
    {
        LoadAsset(PathUtil.GetEffectPath(name), action);
    }
    public void LoadScene(string name, Action<UObject> action)
    {
        LoadAsset(PathUtil.GetScenePath(name), action);
    }


}




