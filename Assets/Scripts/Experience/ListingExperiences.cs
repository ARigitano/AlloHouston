using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CRI.HelloHouston.Experience;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Lists all the available experiences and allows the gamemaster to browse them by audience and difficulty.
    /// </summary>
    public class ListingExperiences : MonoBehaviour
    {

        [SerializeField] private XpGroup[] _allExperiences;                     //All experiences available
        [SerializeField] private XpContext[] _allContexts;                      //The context scriptable objects of all the available experiences
        [SerializeField] private static string _path = "AllExperiences";        //Path of the experiences folder
        [SerializeField] private GameObject _buttonPrefab,                      //Prefab for a button
                                            _dropdownPrefab;                    //Prefab for a dropdown menu
        [SerializeField] private GameObject _panelToAttachButtonsTo,            //Panel for the experiences buttons
                                            _panelToAttachDropdown1To;          //Panel for the contexts dropdown menus
        [SerializeField] private List<string> _contexts;                        //List of contexts for the selected experience
        //
        /// <summary>
        /// Creates a button and attaches it to a vertical layout panel to selecte an experience
        /// </summary>
        /// <param name="name">Name of the selectable experience</param>
        private void CreateButton(string name)
        {
            GameObject button = (GameObject)Instantiate(_buttonPrefab);
            button.transform.SetParent(_panelToAttachButtonsTo.transform);
            button.GetComponent<Button>().onClick.AddListener(() => DisplayContexts(name));
            button.transform.GetChild(0).GetComponent<Text>().text = name;
        }

        /// <summary>
        /// Creates a dropdown menu and attaches it to a vertical layout panel to choose the context of the selected experience
        /// </summary>
        /// <param name="options">Contexts as options for the dropdown menu</param>
        /// <param name="panel">Panel to attach the dropdown menu to</param>
        private void CreateDropDown(string name, List<string> options, GameObject panel)
        {
                GameObject dropdown = (GameObject)Instantiate(_dropdownPrefab);
                dropdown.transform.SetParent(panel.transform);


                dropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "Choose" });

                foreach (string option in options)
                {
                    dropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = option });
                }
            
        }

        /// <summary>
        /// Fetches all the contexts of the selected experience and generates the related dropdown menu
        /// </summary>
        /// <param name="name">Name of the selected experience</param>
        private void DisplayContexts(string name)
        {
                try
                {
                    _allContexts = Resources.LoadAll(_path, typeof(XpContext)).Cast<XpContext>().ToArray();

                    int i = 0;

                    foreach (XpContext context in _allContexts)
                    {
                        if (context.name == name)
                            _contexts.Add(context.context);
                        i++;
                    }

                    CreateDropDown(name, _contexts, _panelToAttachDropdown1To);
                    _contexts.Clear();

                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
        }

        // Use this for initialization
        void Start()
        {
            //Creates a button for each available experience
            try
            {
                _allExperiences = Resources.LoadAll(_path, typeof(XpGroup)).Cast<XpGroup>().ToArray();

                foreach (XpGroup experience in _allExperiences)
                {
                    CreateButton(experience.name);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }
}
