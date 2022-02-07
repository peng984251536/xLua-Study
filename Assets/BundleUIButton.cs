using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleUIButton : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //异步加载出本地的包
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(PathUtil.BundleOutPath + "/prefabs/Button.prefab.ab");
        yield return request;

        AssetBundleCreateRequest request1 = AssetBundle.LoadFromFileAsync(PathUtil.BundleOutPath + "/ui/1.jpg.ab");
        yield return request;

        //异步地从包中加载name的asste
        AssetBundleRequest bundleRequest = request.assetBundle.LoadAssetAsync("Assets/BuildResources/Prefabs/Button.prefab");
        yield return bundleRequest;

        GameObject go = Instantiate(bundleRequest.asset) as GameObject;
        go.transform.SetParent(this.transform);
        go.SetActive(true);
        go.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
