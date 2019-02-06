using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramFeynman : XPHologramElement
    {
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        public MAIAManager manager { get; private set; }
        /// <summary>
        /// The objects containing the Feynman diagrams.
        /// </summary>
        [SerializeField]
        private GameObject[] _feynmanBoxes = null;
        /// <summary>
        /// The line manager.
        /// </summary>
        [SerializeField]
        [Tooltip("The line manager.")]
        private MAIAHologramLineManager _lineManager;

        private Vector3[] _boxPositions;
        private Quaternion[] _boxRotations;

        private void Reset()
        {
            _lineManager = GetComponentInChildren<MAIAHologramLineManager>();
        }

        private void OnEnable()
        {
            ResetPositions();
            foreach (GameObject feynmanBox in _feynmanBoxes)
            {
                feynmanBox.SetActive(true);
            }
        }

        private void OnDisable()
        {
            ResetPositions();
            foreach (GameObject feynmanBox in _feynmanBoxes)
            {
                feynmanBox.SetActive(false);
            }
        }

        public void FillBoxesDiagrams()
        {
            int i = 0;
            foreach (Reaction reaction in manager.settings.allReactions)
            {
                MeshRenderer[] renderers = _feynmanBoxes[i].GetComponentsInChildren<MeshRenderer>();
                renderers[1].material.mainTexture = reaction.diagramImage;
                i++;
            }
        }

        public void ResetPositions()
        {
            for (int i = 0; i < _feynmanBoxes.Length; i++)
            {
                _feynmanBoxes[i].transform.position = _boxPositions[i];
                _feynmanBoxes[i].transform.rotation = _boxRotations[i];
                _feynmanBoxes[i].transform.SetParent(transform);
            }
        }

        private void Init(MAIAManager synchronizer)
        {
            manager = synchronizer;
            _boxPositions = new Vector3[_feynmanBoxes.Length];
            _boxRotations = new Quaternion[_feynmanBoxes.Length];
            for (int i = 0; i < _feynmanBoxes.Length; i++)
            {
                _boxPositions[i] = _feynmanBoxes[i].transform.position;
                _boxRotations[i] = _feynmanBoxes[i].transform.rotation;
            }
            _lineManager.Init();
        }

        public override void OnActivation(XPManager manager)
        {
            base.OnActivation(manager);
            Init((MAIAManager)manager);
            gameObject.SetActive(false);
        }
    }
}
