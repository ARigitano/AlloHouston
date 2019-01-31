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
        private RawImage _currentDiagram = null;
        /// <summary>
        /// All the panels of the tube screen of the experiment.
        /// </summary>
        [SerializeField]
        [Tooltip("All the panels of the tube screen of the experiment.")]
        private GameObject _overrideScreen2 = null;
        /// <summary>
        /// Sprite of the currently selected Feynman diagram.
        /// </summary>
        public Texture diagramSelected { get; private set; }

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
            _currentDiagram.texture = reactions[0].diagramImage;
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