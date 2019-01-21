using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// The top right screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class MAIATubeScreen : XPElement
    {
        /// <summary>
        /// Manager for this experiment.
        /// </summary>
        [Tooltip("Manager for this experiment.")]
        private MAIAManager _manager = null;
        /// <summary>
        /// Thumbnails for all the possible Feynman diagrams.
        /// </summary>
        [SerializeField]
        [Tooltip("Thumbnails for all the possible Feynman diagrams.")]
        private MAIACaseDiagram[] _casesDiagram = null;
        /// <summary>
        /// Image of the current feynman diagram.
        /// </summary>
        [SerializeField]
        [Tooltip("Image of the current feynman diagram.")]
        private Image _currentDiagram = null;
        /// <summary>
        /// All the panels of the tube screen of the experiment.
        /// </summary>
        [SerializeField]
        [Tooltip("All the panels of the tube screen of the experiment.")]
        private GameObject _overrideScreen2 = null;
        /// <summary>
        /// Sprite of the currently selected Feynman diagram.
        /// </summary>
        public Sprite diagramSelected { get; private set; }

        /// <summary>
        /// Called by the synchronizer to skip directly to the Feynman diagrams step.
        /// </summary>
        public void SkipStepOne()
        {
            _overrideScreen2.SetActive(true);
        }

        public void OverrideSecond(Reaction[] reactions)
        {
            _overrideScreen2.SetActive(true);
            _currentDiagram.sprite = reactions[0].diagramImage;
        }

        /// <summary>
        /// Adds a marker on a Feynman diagram thumbnail to indicate that its exits match the ones of the real reaction.
        /// </summary>
        /// <param name="nBDiagram">Index of the diagram to mark.</param>
        public void SelectExit(int nBDiagram)
        {
            if (_casesDiagram[nBDiagram].selectedExits.enabled == true)
                _casesDiagram[nBDiagram].selectedExits.enabled = false;
            else
            {
                _casesDiagram[nBDiagram].selectedExits.enabled = true;
            }
        }

        /// <summary>
        /// Adds a marker on a Feynman diagram thumbnail to indicate that its interactions match the ones of the real reaction.
        /// </summary>
        /// <param name="nBDiagram">Index of the diagram to mark.</param>
        public void SelectInteraction(int nBDiagram)
        {
            if (_casesDiagram[nBDiagram].selectedInteraction.enabled == true)
                _casesDiagram[nBDiagram].selectedInteraction.enabled = false;
            else
                _casesDiagram[nBDiagram].selectedInteraction.enabled = true;
        }

        /// <summary>
        /// Changes the Feynman diagram displayed.
        /// </summary>
        /// <param name="nbDiagram">Index of the diagram to display.</param>
        /// <param name="reactions">All the possible reactions.</param>
        public void OtherDiagram(int nbDiagram, Reaction[] reactions)
        {
            Debug.Log("nbDiagram" + nbDiagram);
            _casesDiagram[nbDiagram].displayed.enabled = true;
            _currentDiagram.sprite = reactions[nbDiagram].diagramImage;
            diagramSelected = reactions[nbDiagram].diagramImage;

            if (nbDiagram == 0)
            {
                _casesDiagram[_casesDiagram.Length-1].displayed.enabled = false;
                _casesDiagram[1].displayed.enabled = false;
            }
            else if (nbDiagram > 0 && nbDiagram < _casesDiagram.Length - 1)
            {
                _casesDiagram[nbDiagram - 1].displayed.enabled = false;
                _casesDiagram[nbDiagram + 1].displayed.enabled = false;
            }
            else
            {
                _casesDiagram[0].displayed.enabled = false;
                _casesDiagram[_casesDiagram.Length - 2].displayed.enabled = false;
            }
        }

        private void Init(MAIAManager synchronizer)
        {
            _manager = synchronizer;
        }

        public override void OnActivation(XPManager manager)
        {
            Init((MAIAManager)manager);
        }
    }
}