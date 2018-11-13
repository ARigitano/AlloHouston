using UnityEngine;
using CRI.HelloHouston.Experience;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// The XpSynchronizer is responsible for the communication of every prefabs of one particular experiment among themselves as well as with the Gamecontroller.
    /// </summary>
    [System.Serializable]
    public abstract class XPSynchronizer : MonoBehaviour
    {
        /// <summary>
        /// Every possible errors that could be displayed on the table screen for the experiment.
        /// </summary>
        [Tooltip("Every possible errors that could be displayed on the table screen for the experiment.")]
        [SerializeField] protected string[] _possibleErrors;
        /// <summary>
        /// All the contents.
        /// </summary>
        public abstract XPContent[] contents { get; }
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
        protected GameManager _gameManager;

        /// <summary>
        /// To be called in case of success of the experiment.
        /// </summary>
        public virtual void OnResolved() {
            foreach (var content in contents)
            {
                content.OnResolved();
            }
        }

        /// <summary>
        /// To be called in case of failure of the experiment.
        /// </summary>
        public virtual void OnFailed() {
            foreach (var content in contents)
            {
                content.OnFailed();
            }
        }
        /// <summary>
        /// To be called to activate the incident of the experiment.
        /// </summary>
        public virtual void Activated() {
            foreach (var content in contents)
            {
                content.OnActivated();
            }
        }

        /// <summary>
        /// To be called to pause the experiment during the game.
        /// </summary>
        public virtual void OnPause()
        {
            foreach (var content in contents)
            {
                content.OnPause();
            }
        }

        /// <summary>
        /// To be called to call back the experiment after it has been paused.
        /// </summary>
        public virtual void OnUnpause()
        {
            foreach (var content in contents)
            {
                content.OnUnpause();
            }
        }
        
        protected virtual void Start()
        {
            error = _possibleErrors[Random.Range(0, _possibleErrors.Length)];
        }

        protected virtual void Update() { }
    }
}
