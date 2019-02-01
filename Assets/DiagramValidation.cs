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
        private MAIATabletScreen _tablet = null;
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
        public List<GameObject> _diagrams = new List<GameObject>();

        /// <summary>
        /// Determines which holographic diagram is counted by the docking zone.
        /// </summary>
        /// <param name="feynmanBox"></param>
        private void ChangeChosenDiagram(GameObject feynmanBox)
        {
            MeshRenderer[] renderers = feynmanBox.GetComponentsInChildren<MeshRenderer>();
            renderers[0].material = _whiteShader;
            Texture diagram = renderers[1].material.mainTexture;
            _tablet.DiagramValidation(diagram);
        }

        void OnTriggerEnter(Collider other)
        {
            //TODO: optimize this garbage
            if (other.tag == "Feynmanbox")
            {
                GameObject feynmanBox = other.gameObject;
                _diagrams.Add(other.gameObject);
                    ChangeChosenDiagram(_diagrams[0]);
               
                

               /* _diagrams.Add(feynmanBox);
                if (_diagrams[0] == feynmanBox)
                { 
                MeshRenderer[] renderers = feynmanBox.GetComponentsInChildren<MeshRenderer>();
                renderers[0].material = _whiteShader;
                Texture diagram = renderers[1].material.mainTexture;
                _tablet.DiagramValidation(diagram);
                }*/
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //TODO: optimize this garbage
            if (other.tag == "Feynmanbox")
            {
                GameObject feynmanBox = other.gameObject;
                _diagrams.Remove(feynmanBox);
                MeshRenderer[] renderers = feynmanBox.GetComponentsInChildren<MeshRenderer>();
                renderers[0].material = _blueShader;
                if (_diagrams.Count == 0)
                {
                    _tablet.NoDiagram();
                }
                else
                {
                    ChangeChosenDiagram(_diagrams[0]);
                    /*MeshRenderer[] renderers2 = _diagrams[0].GetComponentsInChildren<MeshRenderer>();
                    renderers2[0].material = _whiteShader;
                    Texture diagram = renderers2[1].material.mainTexture;
                    _tablet.DiagramValidation(diagram);*/
                }

                
            }
        }
    }
}
