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
        private MAIATabletScreen _tablet; 

        void OnTriggerStay(Collider other)
        {
            if (other.tag == "Feynmanbox")
            {
                GameObject feynmanBox = other.gameObject;
                MeshRenderer[] renderers = feynmanBox.GetComponentsInChildren<MeshRenderer>();
                Texture diagram = renderers[1].material.mainTexture;

                _tablet.DiagramValidation(diagram);
            }
        }
    }
}
