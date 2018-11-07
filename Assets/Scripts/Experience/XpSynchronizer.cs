using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRCalibrationTool;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// The XpSynchronizer is responsible for the communication of every prefabs of one particular experiment among themselves as well as with the Gamecontroller.
    /// </summary>
    public class XpSynchronizer : MonoBehaviour
    {
        /// <summary>
        /// Every possible errors that could be displayed on the table screen for the experiment.
        /// </summary>
        [Tooltip("Every possible errors that could be displayed on the table screen for the experiment.")]
        [SerializeField] protected string[] _possibleErrors;
        /// <summary>
        /// Sound prefabs that will play in case of success or failure of the experiment.
        /// </summary>
        [Tooltip("Sound prefabs that will play in case of success or failure of the experiment.")]
        [SerializeField] protected AudioSource _successSound, _failSound;
        /// <summary>
        /// The error of the experiment chosen randomly that will be displayed on the table screen for this game.
        /// </summary>
        [HideInInspector] public string error;
        /// <summary>
        /// The index of the experiment chosen randomly for this game.                             
        /// </summary>
        [HideInInspector] public string index;
        /// <summary>
        /// The gamemanager that will allow the experiment to communicate with the rest of the station.
        /// </summary>
        //protected GameManager _gameManager;

        /// <summary>
        /// To be called in case of success of the experiment.
        /// </summary>
        public virtual void Resolved()
        {
            _successSound.Play();
        }

        /// <summary>
        /// To be called in case of failure of the experiment.
        /// </summary>
        public virtual void Failed()
        {
            _failSound.Play();
        }

        /// <summary>
        /// To be called to activate the incident of the experiment.
        /// </summary>
        public virtual void Activated()
        {

        }

        /// <summary>
        /// To be called to pause the experiment during the game.
        /// </summary>
        public virtual void Paused()
        {

        }

        /// <summary>
        /// To be called to call bacl the experiment after it has been paused.
        /// </summary>
        public virtual void Unpaused()
        {

        }

        // Use this for initialization
        void Start()
        {
            //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            error = _possibleErrors[Random.Range(0, _possibleErrors.Length)];
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
