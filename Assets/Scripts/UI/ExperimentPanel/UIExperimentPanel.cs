using CRI.HelloHouston.Experience;
using CRI.HelloHouston.Translation;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration.UI
{
    public class UIExperimentPanel : MonoBehaviour
    {
        /// <summary>
        /// Id of the experiment panel.
        /// </summary>
        public int id { get; private set; }
        /// <summary>
        /// The text of the experiment field.
        /// </summary>
        [SerializeField]
        [Tooltip("The text of the experiment field.")]
        private Text _experimentText = null;
        /// <summary>
        /// The text of the wall top field.
        /// </summary>
        [SerializeField]
        [Tooltip("The text of the wall top field.")]
        private Text _wallTopText = null;
        /// <summary>
        /// The text of the wall bottom field.
        /// </summary>
        [SerializeField]
        [Tooltip("The text of the wall bottom field.")]
        private Text _wallBottomText = null;
        /// <summary>
        /// The text of the corner field.
        /// </summary>
        [SerializeField]
        [Tooltip("The text of the corner field.")]
        private Text _cornerText = null;
        /// <summary>
        /// The text of the door field.
        /// </summary>
        [SerializeField]
        [Tooltip("The text of the door field.")]
        private Text _doorText = null;
        /// <summary>
        /// The text of the hologram field.
        /// </summary>
        [SerializeField]
        [Tooltip("The text of the hologram field.")]
        private Text _hologramText = null;
        /// <summary>
        /// The text of the duration field.
        /// </summary>
        [SerializeField]
        [Tooltip("The text of the duration field.")]
        private Text _durationText = null;
        /// <summary>
        /// The context dropdown. It represents all the possible contexts for the experiment.
        /// </summary>
        [SerializeField]
        [Tooltip("The context dropdown. It represents all the possible contexts for the experiment.")]
        private Dropdown _contextDropdown = null;
        /// <summary>
        /// The start toggle. If true, the experiment will start at the launch of the game.
        /// </summary>
        [SerializeField]
        [Tooltip("The start toggle. If true, the experiment will start at the launch of the game.")]
        private Toggle _startToggle = null;
        /// <summary>
        /// The remove button. When clicked on, the experiment will call the destroy method of the Experience Listing.
        /// </summary>
        [SerializeField]
        [Tooltip("The remove button. When clicken on, the experiment will call the destroy method of the Experience Listing.")]
        private Button _removeButton = null;
        /// <summary>
        /// The text key for the choose option of the dropdown.
        /// </summary>
        [SerializeField]
        [Tooltip("The text key for the choose option of the dropdown.")]
        private string _chooseTextKey = "";
        /// <summary>
        /// All the possible contexts for this experiment.
        /// </summary>
        private XPContext[] _contexts;
        /// <summary>
        /// The current selected context of the experiment.
        /// </summary>
        public XPContext currentContext { get; private set; }
        /// <summary>
        /// If true, the experiment will be start at the launch of the game.
        /// </summary>
        public bool start
        {
            get
            {
                return _startToggle.isOn;
            }
        }

        /// <summary>
        /// Initializes the UIExperimentPanel
        /// </summary>
        /// <param name="name">Name of the UIExperimentPanel</param>
        /// <param name="id">ID of the UIExperimentPanel</param>
        /// <param name="totalPanel">The UIExperimentTotalPanel</param>
        /// <param name="listingExperiment">The UIListingExperiments</param>
        /// <param name="experimentPath"></param>
        public void Init(string name, int id,
            UIExperimentTotalPanel totalPanel,
            UIExperimentListing listingExperiment,
            string experimentPath)
        {
            _experimentText.text = name;
            this.id = id;
            totalPanel.AddContext(id);
            LoadAllContexts(name, experimentPath);
            _removeButton.onClick.AddListener(() => { listingExperiment.RemoveExperiment(this); });
            _startToggle.onValueChanged.AddListener((bool val) =>
            {
                listingExperiment.CheckNext();
            });
            _contextDropdown.options.Add(new Dropdown.OptionData() { text = TextManager.instance.GetText(_chooseTextKey) });
            foreach (var option in _contexts)
            {
                _contextDropdown.options.Add(new Dropdown.OptionData() { text = option.contextName });
                _contextDropdown.onValueChanged.AddListener((int value) =>
                {
                    ChooseContext(_contextDropdown.options[value].text);
                    totalPanel.SetContext(id, currentContext);
                    listingExperiment.CheckNext();
                });
            }
        }

        private void LoadAllContexts(string name, string experiencePath)
        {
            try
            {
                var allContextTemp = Resources.LoadAll<XPContext>(experiencePath).Where(x => x.xpGroup.experimentName == name);
                _contexts = allContextTemp.ToArray();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private void ChooseContext(string option)
        {
            currentContext = _contexts.FirstOrDefault(x => x.contextName == option);
            SetPlaceholderText(currentContext);
        }

        private void SetPlaceholderText(XPContext context)
        {
            if (context != null)
            {
                _wallTopText.text = context.totalWallTop.ToString();
                _wallBottomText.text = context.totalWallBottom.ToString();
                _cornerText.text = context.totalCorners.ToString();
                _doorText.text = context.totalDoors.ToString();
                _hologramText.text = context.totalHolograms.ToString();
                _durationText.text = context.xpSettings.duration.ToString();
            }
            else
                ResetAllText();
        }

        private void ResetAllText()
        {
            _wallTopText.text = _wallBottomText.text = _cornerText.text = _doorText.text = _hologramText.text = _durationText.text = "0";
        }
    }
}