using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CRI.HelloHouston.Experience;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListingExperiences : MonoBehaviour {

    [SerializeField] private XpGroup[] _allExperiences;
    [SerializeField] private XpDifficulty[] _allDifficulties;
    [SerializeField] private static string _path = "AllExperiences";
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private GameObject _panelToAttachButtonsTo;

    private void CreateButton(string name)
    {
        GameObject button = (GameObject)Instantiate(_buttonPrefab);
        button.transform.SetParent(_panelToAttachButtonsTo.transform);
        button.GetComponent<Button>().onClick.AddListener(DisplayDifficulties);
        button.transform.GetChild(0).GetComponent<Text>().text = name;
    }

    private void DisplayDifficulties()
    {
        Debug.Log("diffic");
    }

    private void DisplayAudiences()
    {
        Debug.Log("aud");
    }

    // Use this for initialization
    void Start() {

        try
        {
            _allExperiences = Resources.LoadAll(_path, typeof(XpGroup)).Cast<XpGroup>().ToArray();
            _allDifficulties = Resources.LoadAll(_path, typeof(XpDifficulty)).Cast<XpDifficulty>().ToArray();

            foreach (XpGroup experience in _allExperiences)
            {
                CreateButton(experience.name);
            }

            foreach (XpDifficulty difficulty in _allDifficulties)
            {
                CreateButton(difficulty.description);
            }

        } catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
