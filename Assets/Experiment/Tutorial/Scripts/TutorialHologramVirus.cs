using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialHologramVirus : XPHologramElement
    {
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
        public List<Transform> attaches = new List<Transform>();
        [SerializeField]
        private GameObject _virus;
        public int nbVirus = 0;
        public int _maxVirus = 20;

        public void InstantiateVirus(Transform spawning)
        {
            Instantiate(_virus, spawning.position, Quaternion.identity);
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("CountDown");
            StartCoroutine("CountDownVirus");
            GameObject[] attachesArray = GameObject.FindGameObjectsWithTag("CoreAttach");

            foreach (GameObject attach in attachesArray)
            {
                attaches.Add(attach.transform);
            }

            InstantiateVirus(transform);

        }

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
                virusCountDown++;
                if (virusCountDown == 10f)
                {
                    virusCountDown = 5f;
                    //GameObject virus = (GameObject)Instantiate(_virus, gameObject.transform.position, gameObject.transform.rotation);
                    //virus.transform.localScale = gameObject.transform.localScale;
                }
            }
            _uiTimer.text = "Fail";
            tutorialManager.EndMaintenance();
        }

        // Update is called once per frame
        void Update()
        {

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
