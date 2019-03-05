using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// Docking zone for the chosen holographic Feynman diagram
    /// </summary>
    public class DiagramValidation : MonoBehaviour
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
        /// <summary>
        /// List of all the diagrams currently in the docking zone.
        /// </summary>
        private List<MAIAHologramDiagram> _diagrams = new List<MAIAHologramDiagram>();

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

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Feynmanbox")
            {
                MAIAHologramDiagram feynmanBox = other.GetComponent<MAIAHologramDiagram>();
                _diagrams.Add(feynmanBox);
                ChangeChosenDiagram(feynmanBox);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Feynmanbox")
            {
                MAIAHologramDiagram feynmanBox = other.GetComponent<MAIAHologramDiagram>();
                _diagrams.Remove(feynmanBox);
                feynmanBox.screenRenderer.material = _blueShader;
                feynmanBox.displayLine = true;
                if (_diagrams.Count != 0)
                {
                    ChangeChosenDiagram(_diagrams[0]);
                }
            }
        }
    }
}
