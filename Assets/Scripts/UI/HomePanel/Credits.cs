using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Credits.UI
{
    /// <summary>
    /// Displays or hides the credits panel.
    /// </summary>
    public class Credits : MonoBehaviour
    {
        /// <summary>
        /// The credits panel.
        /// </summary>
        [SerializeField]
        private GameObject _creditsPanel;
        /// <summary>
        /// Is the panel enabled or disabled.
        /// </summary>
        private bool isEnabled = false;

        /// <summary>
        /// Enables or disables the panel.
        /// </summary>
        public void PanelEnabler()
        {
            if (!isEnabled)
            {
                _creditsPanel.SetActive(true);
                isEnabled = true;
            }
            else
            {
                _creditsPanel.SetActive(false);
                isEnabled = false;
            }
        }
    }
}
