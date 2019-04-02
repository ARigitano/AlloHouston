using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The hologram for the tutorial experiment.
/// </summary>
namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialHologramSecond : XPHologramElement
    {
        /// <summary>
        /// Text that displays the time remaining before failure.
        /// </summary> 
        [SerializeField]
        private TextMesh _uiTimer;
        /// <summary>
        /// Timer until failure.
        /// </summary>
        [SerializeField]
        private float _timer;
        [SerializeField]
        private Transform[] _attaches;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("CountDown");
        }

        /// <summary>
        /// CountDown until failure.
        /// </summary>
        /// <returns>Wait for seconds.</returns>
        IEnumerator CountDown()
        {
            while (_timer > 0f)
            {
                yield return new WaitForSeconds(1f);
                _timer--;
                _uiTimer.text = _timer.ToString();
            }
            _uiTimer.text = "Fail";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
