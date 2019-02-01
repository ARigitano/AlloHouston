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
        private GameObject[] _feynmanBoxes;

        private Vector3[] _boxPositions;
        private Quaternion[] _boxRotations;
        /// <summary>
        /// 
        /// </summary>
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
                _feynmanBoxes[i].transform.localPosition = _boxPositions[i];
                _feynmanBoxes[i].transform.localRotation = _boxRotations[i];
            }
        }

        private void Init(MAIAManager synchronizer)
        {
            manager = synchronizer;
            _boxPositions = new Vector3[_feynmanBoxes.Length];
            _boxRotations = new Quaternion[_feynmanBoxes.Length];
            for (int i = 0; i < _feynmanBoxes.Length; i++)
            {
                _boxPositions[i] = _feynmanBoxes[i].transform.localPosition;
                _boxRotations[i] = _feynmanBoxes[i].transform.localRotation;
            }   
        }

        public override void OnActivation(XPManager manager)
        {
            Init((MAIAManager)manager);
            base.OnActivation(manager);
            gameObject.SetActive(false);
        }
    }
}
