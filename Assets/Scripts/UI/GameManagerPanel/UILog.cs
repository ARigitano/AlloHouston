using CRI.HelloHouston.Experience;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.UI
{
    internal class UILog : MonoBehaviour
    {
        /// <summary>
        /// The log.
        /// </summary>
        public Log log { get; private set; }
        /// <summary>
        /// The text field of the log message.
        /// </summary>
        [SerializeField]
        [Tooltip("The text field of the log message.")]
        private Text _text = null;
        /// <summary>
        /// The icon of the message.
        /// </summary>
        [SerializeField]
        [Tooltip("The icon of the message.")]
        private Image _icon = null;

        public void Init(string text)
        {
            _text.text = text;
        }

        public void Init(Log log, bool showDate = true)
        {
            this.log = log;
            _text.text = log.ToString(showDate);
        }

        /// <summary>
        /// Refresh log
        /// </summary>
        /// <param name="showDate"></param>
        public void Refresh(bool showDate)
        {
            _text.text = log.ToString(showDate);
        }
    }
}
