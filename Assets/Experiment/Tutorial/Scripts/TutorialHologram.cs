using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The hologram for the tutorial experiment.
/// </summary>
namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialHologram : XPHologramElement
    {
        public TutorialManager tutorialManager { get; private set; }
        //private List<GameObject> _irregularities = new List<GameObject>();
        /// <summary>
        /// Number of irregularities inside the core.
        /// </summary>
        private int _nbIrregularities;
        /// <summary>
        /// Text that displays the number of irregularities remaining inside the core.
        /// </summary>
        [SerializeField]
        private TextMesh _uiNbIrregularities,
        /// <summary>
        /// Text that displays the time remaining before failure.
        /// </summary>  
                        _uiTimer;
        /// <summary>
        /// Timer until failure.
        /// </summary>
        [SerializeField]
        private float _timer;
        private bool win = false;
        public Material materialSuccess, materialFail;
        private int _nbSuccess = 0;

        // Start is called before the first frame update
        void Start()
        {
            foreach(GameObject irregularity in GameObject.FindGameObjectsWithTag("Irregularity"))
            {
                _nbIrregularities++;
            }
            StartCoroutine("CountDown");
        }

        /// <summary>
        /// CountDown until failure.
        /// </summary>
        /// <returns>Wait for seconds.</returns>
        IEnumerator CountDown()
        {
            while(_timer > 0f)
            {
                if (win)
                {
                    tutorialManager.OnIrregularitiesSuccess();
                    break;
                }
                yield return new WaitForSeconds(1f);
                _timer--;
                _uiTimer.text = _timer.ToString();
            }

            if (!win)
            {
                _uiTimer.text = "Fail";
                tutorialManager.EndMaintenance();
            }
        }

        /// <summary>
        /// Update the state of the core each time an irregularity is removed.
        /// </summary>
        public void UpdateNbIrregularities()
        {
            _nbSuccess++;

            if(_nbSuccess * 2 >= _nbIrregularities)
            {
                _uiTimer.text = "winrar";
                win = true;
            }
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
