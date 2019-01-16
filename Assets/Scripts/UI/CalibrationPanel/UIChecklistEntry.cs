using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CRI.HelloHouston.Calibration;

namespace CRI.HelloHouston.Checklist.UI
{
    public class UIChecklistEntry : MonoBehaviour
    {
        /// <summary>
        /// Toggle if entry's sentence has been done.
        /// </summary>
        [Tooltip("Toggle if entry's sentence has been done.")]
        [SerializeField]
        public Toggle _doneToggle = null; 
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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
