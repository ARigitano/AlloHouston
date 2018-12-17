using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.ParticlePhysics
{
    /// <summary>
    /// The top right screen of the experiment block for the particle physics experiment.
    /// </summary>
    public class FakeTubeScreen : XPElement
    {
        /// <summary>
        /// Prefab of the tube.
        /// </summary>
        [SerializeField]
        private GameObject _tube;
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

        public void SelectExit(int nBDiagram)
        {
            if (_casesDiagram[nBDiagram].selectedExits.enabled == true)
                _casesDiagram[nBDiagram].selectedExits.enabled = false;
            else
                _casesDiagram[nBDiagram].selectedExits.enabled = true;
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

            if (nbDiagram - 1 >= 0)
            {
                _casesDiagram[nbDiagram - 1].displayed.enabled = false;
                _previousDiagram.sprite = reactions[nbDiagram - 1].diagramImage;
            }
            else
                _previousDiagram.sprite = null;

            _casesDiagram[nbDiagram].displayed.enabled = true;
            _currentDiagram.sprite = reactions[nbDiagram].diagramImage;

            if (nbDiagram + 1 < _casesDiagram.Length)
            {
                _casesDiagram[nbDiagram + 1].displayed.enabled = false;
                _nextDiagram.sprite = reactions[nbDiagram + 1].diagramImage;
            }
            else
                _nextDiagram.sprite = null;
        }

        //TO DO
        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        public override void OnResolved()
        {
            Debug.Log(name + "Resolved");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        public override void OnFailed()
        {
            Debug.Log(name + "Failed");
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        public override void OnActivated()
        {
            Debug.Log(name + "Activated");
            _tube.GetComponent<MeshRenderer>().enabled = true;
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        public override void OnPause()
        {
            Debug.Log(name + "Paused");
            _tube.GetComponent<MeshRenderer>().enabled = false;
        }
        //TO DO
        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        public override void OnUnpause()
        {
            Debug.Log(name + "Unpaused");
            _tube.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}

