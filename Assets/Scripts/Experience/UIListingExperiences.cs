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
    public class UIListingExperiences : MonoBehaviour
    {
        /// <summary>
        /// All experiences available
        /// </summary>
        private XpGroup[] _allExperiences;
        /// <summary>
        /// The context scriptable objects of all the available experiences
        /// </summary>
        private XpContext[] _allContexts;
        /// <summary>
        /// Path of the experiences folder
        /// </summary>
        private static string _path = "AllExperiences";
        /// <summary>
        /// Prefab for a button
        /// </summary>
        [SerializeField] private GameObject _buttonPrefab = null;
        /// <summary>
        /// Prefab for a dropdown menu
        /// </summary>
        [SerializeField] private GameObject _dropdownPrefab = null;
        /// <summary>
        /// Prefab for an experiment panel
        /// </summary>
        [SerializeField] private GameObject _experimentsPanelPrefab = null;
        /// <summary>
        /// Panel for the experiences buttons
        /// </summary>
        [SerializeField] private GameObject _panelToAttachButtonsTo = null;
        /// <summary>
        /// Panel for the contexts dropdown menus
        /// </summary>
        [SerializeField] private GameObject _panelToAttachDropdown1To = null;
        /// <summary>
        /// List of contexts for the selected experience
        /// </summary>
        private List<XpContext> _contexts = new List<XpContext>();
        /// <summary>
        /// List of contexts of the experiments selected for this game
        /// </summary>
        private List<XpContext> _contextsTotal = new List<XpContext>();
        /// <summary>
        /// The number of experiments that have been added
        /// </summary>
        private int _experimentsCounter = 0;
        /// <summary>
        /// Panel that displays the total number of placeholders and duration
        /// </summary>
        [SerializeField] private ExperiencesTotalPanel _experiencesTotalPanel = null;
        /// <summary>
        /// Number of placeholders offered by the room
        /// </summary>
        public int roomWallTop, roomWallBottom, roomCorner, roomDoor;
        /// <summary>
        /// Button to end the installation of the experiments
        /// </summary>
        [SerializeField] private Button _nextButton = null;                       

        /// <summary>
        /// Removes the selected experiment
        /// </summary>
        /// <param name="panel">The panel of the experiment to be destroyed</param>
        public void RemoveExperiment(ExperimentsPanel panel)
        {
            _contextsTotal.Insert(panel.id, null);
            _contextsTotal.RemoveAt(panel.id+1);
            Destroy(panel.gameObject);
            TotalPlaceholder();
        }

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
            dropdown.transform.SetParent(panel.transform);
            ExperimentsPanel xpPanel = dropdown.GetComponent<ExperimentsPanel>();
            xpPanel.experiment.text = name;
            xpPanel.id = _experimentsCounter;
            xpPanel.removeButton.onClick.AddListener(delegate { RemoveExperiment(xpPanel); });

            _contextsTotal.Add(null);
            _experimentsCounter++;
            
            xpPanel.contexts.options.Add(new Dropdown.OptionData() { text = "Choose" });
            foreach (XpContext option in options)
            {
                xpPanel.contexts.options.Add(new Dropdown.OptionData() { text = option.context });
                xpPanel.contexts.onValueChanged.AddListener(delegate {ChooseContext(xpPanel.contexts.options[xpPanel.contexts.value].text, dropdown); });
            }
        }

        /// <summary>
        /// Checks if there are enough placeholders in the room for a certain category. If not, the user can not end the installation of the experiments.
        /// </summary>
        /// <param name="numberPlaceholder">Number of placeholders required</param>
        /// <param name="numberRoom">Number of placeholders offered by the room</param>
        /// <param name="placeholderText">The text that displays the number of placeholders</param>
        private void NextGray(int numberPlaceholder, int numberRoom, Text placeholderText)
        {
            if (numberPlaceholder > numberRoom)
            {
                placeholderText.fontStyle = FontStyle.BoldAndItalic;
                _nextButton.interactable = false;
                Debug.Log("Not enough placeholders");
            }
            else
            {
                placeholderText.fontStyle = FontStyle.Normal;
                _nextButton.interactable = true;
            }
        }

        /// <summary>
        /// Fills the fields related to the placeholders required by the experiment 
        /// </summary>
        /// <param name="xpPanel">The panel to be filled</param>
        /// <param name="contextSelected">The XpContext of the selected experiment</param>
        public void PlaceholdersTextsFill(ExperimentsPanel xpPanel, XpContext contextSelected)
        {
            if (contextSelected != null)
            {
                if (contextSelected.wallTopZonePrefab.placeholderLeft != null || contextSelected.wallTopZonePrefab.placeholderRight != null || contextSelected.wallTopZonePrefab.placeholderTablet != null)
                {
                    xpPanel.walltop.text = "1";
                }
                else
                {
                    xpPanel.walltop.text = "0";
                }

                if (!contextSelected.wallBottomZonePrefab.bottomPlaceholders.Count.Equals(0))
                {
                    xpPanel.wallbottom.text = contextSelected.wallBottomZonePrefab.bottomPlaceholders.Count.ToString();
                }
                else
                {
                    xpPanel.wallbottom.text = "0";
                }

                if (!contextSelected.cornerZonePrefab.cornerPlaceholders.Count.Equals(0))
                {
                    xpPanel.corner.text = contextSelected.cornerZonePrefab.cornerPlaceholders.Count.ToString();
                }
                else
                {
                    xpPanel.corner.text = "0";
                }

                if (contextSelected.doorZonePrefab.doorPrefab != null)
                {
                    xpPanel.door.text = "1";
                }
                else
                {
                    xpPanel.door.text = "0";
                }

                if(contextSelected.hologramZonePrefab.hologramPrefabs != null)
                {
                    xpPanel.hologram.text = contextSelected.hologramZonePrefab.hologramPrefabs.Length.ToString();
                }
                else
                {
                    xpPanel.hologram.text = "0";
                }

                xpPanel.duration.text = contextSelected.duration.ToString();
            }
            else
            {
                xpPanel.walltop.text = "0";
                xpPanel.wallbottom.text = "0";
                xpPanel.corner.text = "0";
                xpPanel.door.text = "0";
                xpPanel.hologram.text = "0";
                xpPanel.duration.text = "0";
            }
        }

        /// <summary>
        /// Check and displays the total number of placeholders used
        /// </summary>
        private void TotalPlaceholder()
        {
            if (_contextsTotal.Count == 0)
            {
                _nextButton.interactable = false;
            }
            else
            {
                int totalWallTopNumber = 0;
                int totalWallBottomNumber = 0;
                int totalCornerNumber = 0;
                int totalDoorNumber = 0;
                int totalHologramNumber = 0;
                int totalDurationNumber = 0;

                foreach (XpContext context in _contextsTotal)
                {
                    if (context != null)
                    {
                        totalWallTopNumber += 1;
                        totalWallBottomNumber += context.wallBottomZonePrefab.bottomPlaceholders.Count;
                        totalCornerNumber += context.cornerZonePrefab.cornerPlaceholders.Count;
                        if (context.doorZonePrefab.doorPrefab != null)
                            totalDoorNumber += 1;
                        totalHologramNumber += context.hologramZonePrefab.hologramPrefabs.Length;
                        totalDurationNumber += context.duration;
                    }
                }
                _experiencesTotalPanel.totalWallTop.text = totalWallTopNumber.ToString() + "/" + roomWallTop.ToString();
                NextGray(totalWallTopNumber, roomWallTop, _experiencesTotalPanel.totalWallTop);

                _experiencesTotalPanel.totalWallBottom.text = totalWallBottomNumber.ToString() + "/" + roomWallBottom.ToString();
                NextGray(totalWallBottomNumber, roomWallBottom, _experiencesTotalPanel.totalWallBottom);

                _experiencesTotalPanel.totalCorner.text = totalCornerNumber.ToString() + "/" + roomCorner.ToString();
                NextGray(totalCornerNumber, roomCorner, _experiencesTotalPanel.totalCorner);

                _experiencesTotalPanel.totalDoor.text = totalDoorNumber.ToString() + "/" + roomDoor.ToString();
                NextGray(totalDoorNumber, roomDoor, _experiencesTotalPanel.totalDoor);

                _experiencesTotalPanel.totalHologram.text = totalHologramNumber.ToString();

                _experiencesTotalPanel.totalDuration.text = totalDurationNumber.ToString();
            }
        }

        /// <summary>
        /// When the context of an experiment is selected, displays the placeholders required and all other relevant informations on the GameManager canvas.
        /// </summary>
        /// <param name="option">The chosen context of the experiment.</param>
        /// <param name="dropdown">Reference to the dropdown menu which value has been changed.</param>
        public void ChooseContext(string option, GameObject dropdown)
        {
            ExperimentsPanel xpPanel = dropdown.GetComponent<ExperimentsPanel>();

            XpContext contextSelected = null;
            XpContext[] allContextsTemp = null;

            try
            {
                allContextsTemp = Resources.LoadAll(_path, typeof(XpContext)).Cast<XpContext>().ToArray();

                int i = 0;

                foreach (XpContext context in allContextsTemp)
                {
                    if (context.context == option)
                    {
                        contextSelected = context;
                        break;
                    }
                    i++;
                }
                _contextsTotal.RemoveAt(xpPanel.id);
                _contextsTotal.Insert(xpPanel.id, contextSelected);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }

            PlaceholdersTextsFill(xpPanel, contextSelected);

            TotalPlaceholder();
        }

        /// <summary>
        /// Fetches all the contexts of the selected experience and generates the related dropdown menu
        /// </summary>
        /// <param name="name">Name of the selected experience</param>
        public void DisplayContexts(string name)
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
