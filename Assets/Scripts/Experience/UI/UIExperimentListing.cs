using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CRI.HelloHouston.Experience;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using CRI.HelloHouston.Calibration;

namespace CRI.HelloHouston.Experience.UI
{
    /// <summary>
    /// Lists all the available experiences and allows the gamemaster to browse them by audience and difficulty.
    /// </summary>
    public class UIExperimentListing : UIPanel
    {
        /// <summary>
        /// All experiences available
        /// </summary>
        private XPGroup[] _allExperiences;
        /// <summary>
        /// Path of the experiences folder
        /// </summary>
        private static string _path = "AllExperiences";
        /// <summary>
        /// Prefab for a button
        /// </summary>
        [SerializeField] private GameObject _buttonPrefab = null;
        /// <summary>
        /// Prefab for an experiment panel
        /// </summary>
        [SerializeField] private UIExperimentPanel _experimentsPanelPrefab = null;
        /// <summary>
        /// Panel for the experiences buttons
        /// </summary>
        [SerializeField] private Transform _transformToAttachButtonsTO = null;
        /// <summary>
        /// Panel for the contexts dropdown menus
        /// </summary>
        [SerializeField] private Transform _transformToAttachDropdown = null;
        /// <summary>
        /// The number of experiments that have been added
        /// </summary>
        private int _experimentsCounter = 0;
        /// <summary>
        /// Panel that displays the total number of placeholders and duration
        /// </summary>
        [SerializeField] private UIExperimentTotalPanel _experimentTotalPanel = null;
        /// <summary>
        /// All the experiments panel instantiated by the UIListingExperience.
        /// </summary>
        private List<UIExperimentPanel> _experimentPanels = new List<UIExperimentPanel>();
        /// <summary>
        /// Button to end the installation of the experiments
        /// </summary>
        [SerializeField] private UINextButton _nextButton = null;
        /// <summary>
        /// The current room.
        /// </summary>
        private VirtualRoom _currentRoom;                    

        /// <summary>
        /// Removes the selected experiment
        /// </summary>
        /// <param name="panel">The panel of the experiment to be destroyed</param>
        public void RemoveExperiment(UIExperimentPanel panel)
        {
            _experimentTotalPanel.RemoveContext(panel.id);
            _experimentPanels.Remove(panel);
            Destroy(panel.gameObject);
            CheckNext();
        }

        /// <summary>
        /// Creates a button and attaches it to a vertical layout panel to selecte an experience
        /// </summary>
        /// <param name="name">Name of the selectable experience</param>
        private void CreateButton(string name)
        {
            GameObject button = (GameObject)Instantiate(_buttonPrefab, _transformToAttachButtonsTO);
            button.GetComponent<Button>().onClick.AddListener(() => DisplayContexts(name));
            button.transform.GetChild(0).GetComponent<Text>().text = name;
        }

        /// <summary>
        /// Creates a dropdown menu and attaches it to a vertical layout panel to choose the context of the selected experience
        /// </summary>
        /// <param name="options">Contexts as options for the dropdown menu</param>
        /// <param name="panel">Panel to attach the dropdown menu to</param>
        private void CreateExperimentPanel(string name, Transform panel)
        {
            UIExperimentPanel xpPanel = Instantiate(_experimentsPanelPrefab, panel);
            xpPanel.Init(name, _experimentsCounter, _experimentTotalPanel, this, _path);
            _experimentPanels.Add(xpPanel);
            CheckNext();
            _experimentsCounter++;
        }

        /// <summary>
        /// Fetches all the contexts of the selected experience and generates the related dropdown menu
        /// </summary>
        /// <param name="name">Name of the selected experience</param>
        public void DisplayContexts(string name)
        {
            CreateExperimentPanel(name, _transformToAttachDropdown);
        }

        /// <summary>
        /// Checks if the next button is interactable.
        /// </summary>
        public void CheckNext()
        {
            int totalWallTop = 0;
            bool emptyPanel = false;
            foreach (UIExperimentPanel panel in _experimentPanels)
            {
                if (panel.start && panel.currentContext != null)
                {
                    totalWallTop += panel.currentContext.totalWallTop;
                }
                if (panel.currentContext == null)
                {
                    emptyPanel = true;
                    break;
                }
            }
            Debug.Log(string.Format("{0} {1} {2} {3}", totalWallTop, !_experimentTotalPanel.overflow, emptyPanel, _experimentPanels.Count));
            _nextButton.interactable = !_experimentTotalPanel.overflow && totalWallTop <= _currentRoom.GetZones(ZoneType.WallTop).Length && !emptyPanel;
            if (_nextButton.interactable)
                _nextButton.nextObject = new RoomXPPair(_currentRoom, _experimentPanels.Select(x => x.currentContext).ToArray());
        }

        public override void Init(object obj)
        {
            base.Init(obj);
            Init((VirtualRoom)obj);
        }

        // Use this for initialization
        public void Init(VirtualRoom room)
        {
            //Creates a button for each available experience
            try
            {
                _allExperiences = Resources.LoadAll(_path, typeof(XPGroup)).Cast<XPGroup>().ToArray();
                _currentRoom = room;
                _experimentTotalPanel.Init(room);

                foreach (XPGroup experience in _allExperiences)
                {
                    CreateButton(experience.experimentName);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
