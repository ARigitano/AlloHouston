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

        [Tooltip("Every possible errors that could be displayed on the table screen for the experiment.")]
        [SerializeField] protected string[] _possibleErrors;                //Every possible errors that could be displayed on the table screen for the experiment.

        [Tooltip("Sound prefabs that will play in case of success or failure of the experiment.")]
        [SerializeField] protected AudioSource _successSound, _failSound;   //Sound prefabs that will play in case of success or failure of the experiment.

        [HideInInspector] public string error;                                                //The error of the experiment chosen randomly that will be displayed on the table screen for this game.
        [HideInInspector] public string index;                                                //The index of the experiment chosen randomly for this game.                             

        protected GameManager _gameManager;                                 //The gamemanager that will allow the experiment to communicate with the rest of the station.

        //To be called in case of success of the experiment.
        public virtual void Resolved()
        {
            _successSound.Play();
        }

        //To be called in case of failure of the experiment.
        public virtual void Failed()
        {
            _failSound.Play();
        }

        //To be called to activate the incident of the experiment.
        public virtual void Activated()
        {

        }

        //To be called to pause the experiment during the game.
        public virtual void Paused()
        {

        }

        //To be called to call bacl the experiment after it has been paused.
        public virtual void Unpaused()
        {

        }

        // Use this for initialization
        void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            error = _possibleErrors[Random.Range(0, _possibleErrors.Length)];
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
