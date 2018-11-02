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
                                            _dropdownPrefab,                    //Prefab for a dropdown menu
                                            _experimentsPanelPrefab;            //Prefab for an experiment panel
        [SerializeField] private GameObject _panelToAttachButtonsTo,            //Panel for the experiences buttons
                                            _panelToAttachDropdown1To;          //Panel for the contexts dropdown menus
        [SerializeField] private List<XpContext> _contexts;                        //List of contexts for the selected experience
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
        private void CreateDropDown(string name, List<XpContext> options, GameObject panel)
        {
                GameObject dropdown = (GameObject)Instantiate(_experimentsPanelPrefab);
            ExperimentsPanel xpPanel = dropdown.GetComponent<ExperimentsPanel>();
            xpPanel.experiment.text = name;
            
           /*if(options.wallTopZonePrefab.placeholderLeft != null || options.wallTopZonePrefab.placeholderRight != null || options.wallTopZonePrefab.placeholderTablet != null)
            {
                xpPanel.walltop.text = "1";
            }

            if (options.doorZonePrefab.doorPrefab != null)
            {
                xpPanel.door.text = "1";
            }

            if (options.wallBottomZonePrefab.bottomPlaceholders != null)
            {
                xpPanel.wallbottom.text = options.wallBottomZonePrefab.bottomPlaceholders.Count.ToString();
            }

            if(options.cornerZonePrefab.cornerPlaceholders != null)
            {
                xpPanel.corner.text = options.cornerZonePrefab.cornerPlaceholders.Count.ToString();
            }*/

            xpPanel.contexts.options.Add(new Dropdown.OptionData() { text = "Choose" });
            foreach (XpContext option in options)
            {
                xpPanel.contexts.options.Add(new Dropdown.OptionData() { text = option.context });
                xpPanel.contexts.onValueChanged.AddListener(delegate { ChooseContext(option, dropdown); });
            }
            


            dropdown.transform.SetParent(panel.transform);


                //dropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "Choose" });

                /*foreach (string option in options)
                {
                    dropdown.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = option });
                }*/
            
        }

        public void ChooseContext(XpContext option, GameObject dropdown)
        {

            Debug.Log(option.context);

            ExperimentsPanel xpPanel = dropdown.GetComponent<ExperimentsPanel>();
            xpPanel.experiment.text = name;

            


            if (option.wallTopZonePrefab.placeholderLeft != null || option.wallTopZonePrefab.placeholderRight != null || option.wallTopZonePrefab.placeholderTablet != null)
             {
                 xpPanel.walltop.text = "1";
             } else
            {
                xpPanel.walltop.text = "0";
            }

             if (!option.wallBottomZonePrefab.bottomPlaceholders.Count.Equals(0))
             {
                 xpPanel.wallbottom.text = option.wallBottomZonePrefab.bottomPlaceholders.Count.ToString();
             }
           /* else
            {
                xpPanel.wallbottom.text = "0";
            }*/

            if (!option.cornerZonePrefab.cornerPlaceholders.Count.Equals(0))
             {
                xpPanel.corner.text = option.cornerZonePrefab.cornerPlaceholders.Count.ToString();
             }
            else
            {
                xpPanel.corner.text = "0";
            }

            if (option.doorZonePrefab.doorPrefab != null)
            {
                xpPanel.door.text = "1";
            }
            else
            {
                xpPanel.corner.text = "0";
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
                    {
                        _contexts.Add(context);
                        i++;
                    }
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
