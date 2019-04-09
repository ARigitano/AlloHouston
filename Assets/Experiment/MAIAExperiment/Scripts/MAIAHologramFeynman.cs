using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramFeynman : XPHologramElement
    {
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        public MAIAManager maiaManager { get; private set; }
        /// <summary>
        /// The objects containing the Feynman diagrams.
        /// </summary>
        [SerializeField]
        private MAIAHologramDiagram[] _feynmanBoxes = null;
        /// <summary>
        /// The line manager.
        /// </summary>
        [SerializeField]
        [Tooltip("The line manager.")]
        private MAIAHologramLineManager _lineManager;

        private Vector3[] _boxPositions;
        private Quaternion[] _boxRotations;
        /// <summary>
        /// Random generated with the GameManager's seed.
        /// </summary>
        private System.Random _rand;

        private void Reset()
        {
            _lineManager = GetComponentInChildren<MAIAHologramLineManager>();
        }

        private void OnEnable()
        {
            foreach(MAIAHologramDiagram feynmanBox in _feynmanBoxes)
            {
                feynmanBox.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            foreach (MAIAHologramDiagram feynmanBox in _feynmanBoxes)
            {
                feynmanBox.gameObject.SetActive(false);
            }
        }

        public void FillBoxesDiagrams()
        {
            IList<Reaction> allReactions = maiaManager.settings.allReactions.Shuffle(_rand);
            for (int i = 0; i < allReactions.Count && i < _feynmanBoxes.Length; i++)
            {
                _feynmanBoxes[i].contentRenderer.material.mainTexture = allReactions[i].diagramImage;
                _feynmanBoxes[i].name = "hologram_content: " + allReactions[i].diagramImage.name;
                _feynmanBoxes[i].displayLine = true;
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
            maiaManager = synchronizer;
            _boxPositions = new Vector3[_feynmanBoxes.Length];
            _boxRotations = new Quaternion[_feynmanBoxes.Length];
            for (int i = 0; i < _feynmanBoxes.Length; i++)
            {
                _boxPositions[i] = _feynmanBoxes[i].transform.position;
                _boxRotations[i] = _feynmanBoxes[i].transform.rotation;
            }
            _lineManager.Init(_feynmanBoxes);
        }

        public override void OnShow(int currentStep)
        {
            FillBoxesDiagrams();
        }

        public override void OnInit(XPManager manager, int randomSeed)
        {
            base.OnInit(manager, randomSeed);
            _rand = new System.Random(randomSeed);
            Init((MAIAManager)manager);
        }

        public override void OnActivation()
        {
            base.OnActivation();
            gameObject.SetActive(false);
        }
    }
}
