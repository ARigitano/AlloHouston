using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The first hologram for the tutorial experiment.
/// </summary>
namespace CRI.HelloHouston.Experience.Tutorial
{
    public class TutorialHologram : XPHologramElement
    {
        /// <summary>
        /// The manager for the experiment.
        /// </summary>
        public TutorialManager tutorialManager { get; private set; }
        /// <summary>
        /// Number of irregularities inside the core.
        /// </summary>
        private int _nbIrregularities;
        /// <summary>
        /// Text that displays the time remaining before failure.
        /// </summary>  
        [SerializeField]
        private TextMesh _uiTimer;
        /// <summary>
        /// Time until failure.
        /// </summary>
        [SerializeField]
        private float _timer;
        /// <summary>
        /// Is the hologram resolved?
        /// </summary>
        private bool _win = false;
        /// <summary>
        /// Materials for the buildings in case of correct or wrong irregularity removed
        /// </summary>
        public Material materialSuccess, materialFail;
        /// <summary>
        /// The number of irregularities correctly removed
        /// </summary>
        private int _nbSuccess = 0;
        public bool startTimer = false;

        // Start is called before the first frame update
        void Start()
        {
            _nbIrregularities = GameObject.FindGameObjectsWithTag("Irregularity").Length;
        }

        /// <summary>
        /// CountDown until failure.
        /// </summary>
        /// <returns>Wait for seconds.</returns>
        IEnumerator CountDown()
        {
            while(_timer > 0f)
            {
                if (_win)
                {
                    tutorialManager.OnIrregularitiesSuccess();
                    break;
                }
                yield return new WaitForSeconds(1f);
                _timer--;
                _uiTimer.text = _timer.ToString();
            }

            if (!_win)
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
                _win = true;
                tutorialManager.EndMaintenance();
            }
        }

        public void StartCounter()
        {
            StartCoroutine(CountDown());
        }

        public override void Show()
        {
            base.Show();
            StopAllCoroutines();
            
        }

        public override void Hide()
        {
            base.Hide();
            StopAllCoroutines();
        }

        public override void OnShow(int currentStep)
        {
            base.OnActivation();
            visible = false;
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
