using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAOverview : MonoBehaviour
    {
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        [Tooltip("Script for the whole top screen.")]
        private MAIATopScreen _maiaTopScreen;
        /// <summary>
        /// Error popup.
        /// </summary>
        [SerializeField]
        [Tooltip("Error popup.")]
        private GameObject _errorMessage = null;
        /// <summary>
        /// Info message.
        /// </summary>
        [SerializeField]
        [Tooltip("Info message popup.")]
        private GameObject _infoMessage = null;
    }
}
