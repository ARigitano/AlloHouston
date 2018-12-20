using CRI.HelloHouston.ParticlePhysics;
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
        /// 
        /// </summary>
        [SerializeField]
        private CaseDiagram[] _casesDiagram;
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private Image _previousDiagram, _currentDiagram, _nextDiagram;
        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private GameObject _overrideScreen2;
        public Sprite diagramSelected;
        /// <summary>
        /// Synchronizer for this experiment.
        /// </summary>
        [SerializeField]
        private MAIASynchronizer _synchronizer;


        public void OverrideSecond(Reaction[] reactions)
        {
            _overrideScreen2.SetActive(true);
            _currentDiagram.sprite = reactions[0].diagramImage;
        }

        public void SelectExit(int nBDiagram)
        {
            if (_casesDiagram[nBDiagram].selectedExits.enabled == true)
                _casesDiagram[nBDiagram].selectedExits.enabled = false;
            else
            {
                _casesDiagram[nBDiagram].selectedExits.enabled = true;
            }
        }

        public void SelectInteraction(int nBDiagram)
        {
            if (_casesDiagram[nBDiagram].selectedInteraction.enabled == true)
                _casesDiagram[nBDiagram].selectedInteraction.enabled = false;
            else
                _casesDiagram[nBDiagram].selectedInteraction.enabled = true;
        }

        public void OtherDiagram(int nbDiagram, Reaction[] reactions)
        {

            _casesDiagram[nbDiagram].displayed.enabled = true;
            _currentDiagram.sprite = reactions[nbDiagram].diagramImage;
            diagramSelected = reactions[nbDiagram].diagramImage;

            if (nbDiagram - 1 < 0)
            {
                nbDiagram = _casesDiagram.Length;
                _casesDiagram[nbDiagram - 1].displayed.enabled = false;
            }

            if (nbDiagram + 1 > _casesDiagram.Length)
            {
                nbDiagram = 0;
                _casesDiagram[nbDiagram + 1].displayed.enabled = false;
            }
        }

        public void Init(MAIASynchronizer synchronizer)
        {
            _synchronizer = synchronizer;
        }
    }
}