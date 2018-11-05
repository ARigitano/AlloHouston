using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CRI.HelloHouston.Experience;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CRI.HelloHouston.Experience
{
    public class UIListingExperiences : MonoBehaviour
    {
        [SerializeField]
        private GameObject _buttonPrefab,                      //Prefab for a button
                                            

                                            _experimentsPanelPrefab;            //Prefab for an experiment panel
        [SerializeField]
        private GameObject _panelToAttachButtonsTo;            //Panel for the experiences buttons
                                            
        
        [SerializeField] private List<XpContext> _contextsTotal;                //List of contexts of the experiments selected for this game
        [SerializeField] private int _experimentsCounter = 0;                   //The number of experiments that have been added
        [SerializeField] private ExperiencesTotalPanel _experiencesTotalPanel;  //Panel that displays the total number of placeholders and duration
        [SerializeField]
        private int _totalWallTopNumber,                       //The total number of wall top placeholders required
                                    _totalWallBottomNumber,                     //The total number of wall bottom placeholders required
                                    _totalCornerNumber,                         //The total number of corner placeholders required
                                    _totalDoorNumber,                           //The total number of door placeholders required
                                    _totalDurationNumber;                       //The total estimated duration of the game
        public int roomWallTop, roomWallBottom, roomCorner, roomDoor;           //Number of placeholders offered by the room
        [SerializeField] private Button _nextButton;                            //Button to end the installation of the experiments
        [SerializeField] private ListingExperiences _listingExperiences;

        /// <summary>
        /// Removes the selected experiment
        /// </summary>
        /// <param name="panel">The panel of the experiment to be destroyed</param>
        public void RemoveExperiment(ExperimentsPanel panel)
        {
            _contextsTotal.RemoveAt(panel.id);
            Destroy(panel.gameObject);
        }

        /// <summary>
        /// Creates a button and attaches it to a vertical layout panel to selecte an experience
        /// </summary>
        /// <param name="name">Name of the selectable experience</param>
        public void CreateButton(string name)
        {
            GameObject button = (GameObject)Instantiate(_buttonPrefab);
            button.transform.SetParent(_panelToAttachButtonsTo.transform);
            button.GetComponent<Button>().onClick.AddListener(() => _listingExperiences.DisplayContexts(name));
            button.transform.GetChild(0).GetComponent<Text>().text = name;
        }

        /// <summary>
        /// Creates a dropdown menu and attaches it to a vertical layout panel to choose the context of the selected experience
        /// </summary>
        /// <param name="options">Contexts as options for the dropdown menu</param>
        /// <param name="panel">Panel to attach the dropdown menu to</param>
        public void CreateDropDown(string name, List<XpContext> options, GameObject panel)
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
                xpPanel.contexts.onValueChanged.AddListener(delegate { _listingExperiences.ChooseContext(xpPanel.contexts.options[xpPanel.contexts.value].text, dropdown); });
            }
        }
    }
}
