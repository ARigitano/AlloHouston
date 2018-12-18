using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour {

    AssetBundle myLoadedAssetBundle;
    public string path;
    public string assetName;

	// Use this for initialization
	void Start () {
        LoadAssetBundle(path);
        InstantiateObjectsFromBundle(assetName);
	}

    void LoadAssetBundle(string bundleURL)
    {
        myLoadedAssetBundle = AssetBundle.LoadFromFile(bundleURL);

        Debug.Log(myLoadedAssetBundle == null ? "Failed to load" : "Loading successful");
    }

    void InstantiateObjectsFromBundle(string assetName)
    {
        var prefab = myLoadedAssetBundle.LoadAsset(assetName);
        Instantiate(prefab);
    }
}
