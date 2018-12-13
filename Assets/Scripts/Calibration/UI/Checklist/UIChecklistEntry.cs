using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Calibration.UI
{
    public class UIChecklistEntry : MonoBehaviour
    {
        /// <summary>
        /// Toggle if entry's sentence has been done.
        /// </summary>
        [Tooltip("Toggle if entry's sentence has been done.")]
        [SerializeField]
        public Toggle doneToggle = null;
        /// <summary>
        /// Text field of the entry's sentence.
        /// </summary>
        [Tooltip("Text field of the entry's sentence.")]
        [SerializeField]
        private Text _sentenceText = null;

        public void Init(string check)
        {
            _sentenceText.text = check;
        }
    }
}
