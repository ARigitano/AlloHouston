using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramDiagram : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The renderer of the screen.")]
        private MeshRenderer _screenRenderer = null;
        /// <summary>
        /// The renderer of the screen.
        /// </summary>
        public MeshRenderer screenRenderer
        {
            get
            {
                return _screenRenderer;
            }
        }
        [SerializeField]
        [Tooltip("The renderer of the content.")]
        private MeshRenderer _contentRenderer = null;
        /// <summary>
        /// The renderer of the content.
        /// </summary>
        public MeshRenderer contentRenderer
        {
            get
            {
                return _contentRenderer;
            }
        }
        [SerializeField]
        [Tooltip("Anchor point for the line.")]
        private Transform _anchorPoint = null;
        /// <summary>
        /// Anchor point for the line.
        /// </summary>
        public Transform anchorPoint
        {
            get
            {
                return _anchorPoint;
            }
        }

        public bool displayLine { get; set; }

        private void Reset()
        {
            var meshRenderers = GetComponentsInChildren<MeshRenderer>();
            int length = meshRenderers.Length;
            if (length > 0)
                _screenRenderer = meshRenderers[0];
            if (length > 1)
                _contentRenderer = meshRenderers[1];
        }
    }
}
