using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using CRI.HelloHouston.Experience;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration.UI
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
        /// Path of the experience group folder
        /// </summary>
        private static string _groupPath = "AllExperiences";
        /// <summary>
        /// Path of the experience context folder
        /// </summary>
        private static string _experimentPath = "AllExperiences";
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
        [SerializeField] private Button _nextButton = null;
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
            xpPanel.Init(name, _experimentsCounter, _experimentTotalPanel, this, _experimentPath);
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
            bool emptyPanel = _experimentPanels.Any(panel => panel.currentContext == null);
            _nextButton.interactable = !_experimentTotalPanel.overflow && !emptyPanel;
            if (_nextButton.interactable)
                _nextObject = new RoomXPPair(_currentRoom, _experimentPanels.Select(x => x.currentContext).ToArray(), _experimentPanels.Select(x => x.start).ToArray());
        }

        public override void Init(object obj)
        {
            _nextButton.onClick.AddListener(Next);
            Init((VirtualRoom)obj);
        }

        // Use this for initialization
        public void Init(VirtualRoom room)
        {
            //Creates a button for each available experience
            try
            {
                _allExperiences = Resources.LoadAll<XPGroup>(_groupPath).ToArray();
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
