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
    [SerializeField] private GameObject _buttonPrefab, _dropdownPrefab;
    [SerializeField] private GameObject _panelToAttachButtonsTo, _panelToAttachDropdown1To, _panelToAttachDropdown2To;
    [SerializeField] private List<string> _difficulties;

    private void CreateButton(string name)
    {
        GameObject button = (GameObject)Instantiate(_buttonPrefab);
        button.transform.SetParent(_panelToAttachButtonsTo.transform);
        button.GetComponent<Button>().onClick.AddListener(() => DisplayDifficulties(name, "Audience", ""));
        button.transform.GetChild(0).GetComponent<Text>().text = name;
    }

    private void CreateDropDown(List<string> options, GameObject panel, bool onValueChanged)
    {
        GameObject dropdown = (GameObject)Instantiate(_dropdownPrefab);
        dropdown.transform.SetParent(panel.transform);

        dropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "Choose" });

        foreach (string option in options)
        {
           dropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = option });
            
            dropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { DisplayDifficulties("ee", "Difficulty", "Casual"); });
        }


        
        
    }

    private void DisplayDifficulties(string name, string parameter, string audience)
    {
        if (parameter == "Audience")
        {
            try
            {
                _allDifficulties = Resources.LoadAll(_path, typeof(XpDifficulty)).Cast<XpDifficulty>().ToArray();

                int i = 0;

                foreach (XpDifficulty difficulty in _allDifficulties)
                {
                    if (difficulty.name == name)
                        _difficulties.Add(difficulty.audienceType.ToString());
                    i++;
                }

                CreateDropDown(_difficulties, _panelToAttachDropdown1To, true);
                _difficulties.Clear();

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
        else if (parameter == "Difficulty")
        {

            try
            {
                _allDifficulties = Resources.LoadAll(_path, typeof(XpDifficulty)).Cast<XpDifficulty>().ToArray();

                int i = 0;

                foreach (XpDifficulty difficulty in _allDifficulties)
                {
                    if (difficulty.name == name && difficulty.audienceType.ToString() == audience)
                        _difficulties.Add(difficulty.difficultyType.ToString());
                    i++;
                }

                CreateDropDown(_difficulties, _panelToAttachDropdown2To, false);
                _difficulties.Clear();

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

    }



    // Use this for initialization
    void Start() {

        try
        {
            _allExperiences = Resources.LoadAll(_path, typeof(XpGroup)).Cast<XpGroup>().ToArray();

            foreach (XpGroup experience in _allExperiences)
            {
                CreateButton(experience.name);
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
