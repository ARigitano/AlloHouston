using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    /// <summary>
    /// The second hologram for the tutorial experiment.
    /// </summary>
    public class TutorialHologramVirus : XPHologramElement
    {
        /// <summary>
        /// The manager for the experiment.
        /// </summary>
        public TutorialManager tutorialManager { get; private set; }
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
        /// <summary>
        /// List of the points toward wich the viruses can move
        /// </summary>
        public List<Transform> attaches = new List<Transform>();
        /// <summary>
        /// Prefab for the virus
        /// </summary>
        [SerializeField]
        private GameObject _virus;
        /// <summary>
        /// Number of viruses that have been instantiated so far
        /// </summary>
        public int nbVirus = 0;
        /// <summary>
        /// Maximum number of viruses that can be instantiated
        /// </summary>
        public int _maxVirus = 20;

        /// <summary>
        /// Instantiates one virus
        /// </summary>
        /// <param name="spawning"></param>
        public void InstantiateVirus(Transform spawning)
        {
            Instantiate(_virus, spawning.position, Quaternion.identity);
        }

        // Start is called before the first frame update
        void Start()
        {
            
            GameObject[] attachesArray = GameObject.FindGameObjectsWithTag("CoreAttach");

            foreach (GameObject attach in attachesArray)
            {
                attaches.Add(attach.transform);
            }

            StartCoroutine("CountDown");
            InstantiateVirus(transform);
            //StartCoroutine("CountDownVirus");
        }

        /// <summary>
        /// Regularly instantiates a new virus
        /// </summary>
        /// <returns></returns>
        IEnumerator CountDownVirus()
        {
            float virusCountDown = 0f;
            while (_timer > 0f)
            {
                yield return new WaitForSeconds(1f);
                virusCountDown++;
                if (virusCountDown == 3f)
                {
                    InstantiateVirus(transform);
                    virusCountDown = 0;
                }
            }

        }

        /// <summary>
        /// CountDown until failure.
        /// </summary>
        /// <returns>Wait for seconds.</returns>
        IEnumerator CountDown()
        {
            float virusCountDown = 0f;
            while (_timer > 0f)
            {
                yield return new WaitForSeconds(1f);
                _timer--;
                _uiTimer.text = _timer.ToString();
            }
            _uiTimer.text = "Fail";
            tutorialManager.EndMaintenance();
        }

        public override void OnShow(int currentStep)
        {
            base.OnActivation();
            gameObject.SetActive(false);
        }

        private void Init(TutorialManager synchronizer)
        {
            tutorialManager = synchronizer;
        }

        public override void OnInit(XPManager manager, int randomSeed)
        {
            base.OnInit(manager, randomSeed);
            Init((TutorialManager)manager);
        }
    }
}
