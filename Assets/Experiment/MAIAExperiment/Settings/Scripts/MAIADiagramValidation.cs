using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// Docking zone for the chosen holographic Feynman diagram
    /// </summary>
    public class MAIADiagramValidation : MonoBehaviour
    {
        /// <summary>
        /// The tabletScreen script.
        /// </summary>
        [SerializeField]
        private MAIAReactionPanel _reactionPanel = null;
        /// <summary>
        /// A blue shader for not counted diagrams.
        /// </summary>
        [SerializeField]
        private Material _blueShader = null;
        /// <summary>
        /// A white diagram for the counted diagram.
        /// </summary>
        [SerializeField]
        private Material _whiteShader = null;
        [SerializeField]
        [Tooltip("The snap drop zone attached to this diagram validation.")]
        private VRTK_SnapDropZone _snapDropZone = null;
        /// <summary>
        /// List of all the diagrams currently in the docking zone.
        /// </summary>
        private List<MAIAHologramDiagram> _diagrams = new List<MAIAHologramDiagram>();

        private void Reset()
        {
            _snapDropZone = GetComponentInChildren<VRTK_SnapDropZone>();
        }

        private void Start()
        {
            if (_snapDropZone == null)
                _snapDropZone = GetComponentInChildren<VRTK_SnapDropZone>();
            _snapDropZone.ObjectSnappedToDropZone += ObjectSnappedToDropZone;
            _snapDropZone.ObjectUnsnappedFromDropZone += ObjectUnsnappedFromDropZone;
        }

        private void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
        {
            if (e.snappedObject.tag == "Feynmanbox" )
            {
                MAIAHologramDiagram feynmanBox = e.snappedObject.GetComponent<MAIAHologramDiagram>();
                _diagrams.Add(feynmanBox);
                ChangeChosenDiagram(feynmanBox);
            }
        }

        private void ObjectUnsnappedFromDropZone(object sender, SnapDropZoneEventArgs e)
        {
            if (e.snappedObject.tag == "Feynmanbox")
            {
                MAIAHologramDiagram feynmanBox = e.snappedObject.GetComponent<MAIAHologramDiagram>();
                _diagrams.Remove(feynmanBox);
                feynmanBox.screenRenderer.material = _blueShader;
                feynmanBox.displayLine = true;
                if (_diagrams.Count != 0)
                {
                    ChangeChosenDiagram(_diagrams[0]);
                }
            }
        }

        /// <summary>
        /// Determines which holographic diagram is counted by the docking zone.
        /// </summary>
        /// <param name="feynmanBox"></param>
        private void ChangeChosenDiagram(MAIAHologramDiagram feynmanBox)
        {
            feynmanBox.screenRenderer.material = _whiteShader;
            feynmanBox.displayLine = false;
            Texture diagram = feynmanBox.contentRenderer.material.mainTexture;
            _reactionPanel.selectedDiagram = diagram;
        }

        private void OnDisable()
        {
            _snapDropZone.ForceUnsnap();
        }
    }
}
