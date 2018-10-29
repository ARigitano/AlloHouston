using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class ExperiencesListing : MonoBehaviour {

    [SerializeField]private static string _path = "Assets/Resources/AllExperiences";
    private static string _searchPattern = "*.asset";
    private static DirectoryInfo _dir = new DirectoryInfo(_path);
    private DirectoryInfo[] _dirs;
    [SerializeField] FileInfo[] _info;
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _panelToAttachButtonsTo;

    private void CreateButton(string name)
    {
        GameObject button = (GameObject)Instantiate(_buttonPrefab);
        button.transform.SetParent(_panelToAttachButtonsTo.transform);
        //button.GetComponent<Button>().onClick.AddListener(OnClick);
        button.transform.GetChild(0).GetComponent<Text>().text = name;
    }

    // Use this for initialization
    void Start () {

        try
        {
            if(_dir.Exists)
            {
                _dirs = _dir.GetDirectories("*", SearchOption.TopDirectoryOnly);
                _info = _dir.GetFiles("*.asset", SearchOption.TopDirectoryOnly);

                /*foreach (FileInfo f in _info) {*/

                foreach(DirectoryInfo dir in _dirs) { 

                    CreateButton(dir.Name);

                    Debug.Log(dir.Name);
                }
            } else
            {
                Debug.Log("Folder not found");
            }
        } catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
