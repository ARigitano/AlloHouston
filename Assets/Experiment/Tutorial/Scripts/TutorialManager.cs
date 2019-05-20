using CRI.HelloHouston.Calibration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace CRI.HelloHouston.Experience.Tutorial
{
    /// <summary>
    /// The XPManager for the tutorial experiment.
    /// </summary>
    public class TutorialManager : XPManager
    {
        /// <summary>
        /// The top left script of the experiment block.
        /// </summary>
        public TutorialTopScreen topScreen { get; private set; }
        /// <summary>
        /// The top right script of the experiment block.
        /// </summary>
        public TutorialTubeScreen tubeScreen { get; private set; }
        /// <summary>
        /// The tablet script of the experiment block.
        /// </summary>
        public TutorialTabletScreen tabletScreen { get; private set; }
        /// <summary>
        /// The first hologram of the table block.
        /// </summary>
        public TutorialHologram hologram { get; private set; }
        /// <summary>
        /// The second hologram of the table block.
        /// </summary>
        public TutorialHologramVirus hologramSecond { get; private set; }
        /// <summary>
        /// Settings of the experience.
        /// </summary>
        public TutorialSettings settings { get; private set; }
        /// <summary>
        /// Player for the intro video
        /// </summary>
        [SerializeField]
        private VideoPlayer _introVideo;
        /// <summary>
        /// Can the holocube be initialized?
        /// </summary>
        private bool _cubeInitializable = false;
        /// <summary>
        /// Can the communication screen be initialized?
        /// </summary>
        private bool _comInitializable = false;
        /// <summary>
        /// Can the maintenance process be launched?
        /// </summary>
        private bool _maintenanceLaunchable = false;
        /// <summary>
        /// Can the maintenance process be started?
        /// </summary>
        private bool _maintenanceStartable = false;
        /// <summary>
        /// Is it the end of the tutorial?
        /// </summary>
        private bool _isEnd = false;
        /// <summary>
        /// Prefab for the introduction screen
        /// </summary>
        [SerializeField]
        private GameObject _screenPrefab;
        /// <summary>
        /// Instantiated introduction screen
        /// </summary>
        private GameObject _screenInstance;
        /// <summary>
        /// Dock for the holocube
        /// </summary>
        private CubeDock _dock;
        /// <summary>
        /// Position on which the introduction screen should be instantiated
        /// </summary>
        private GameObject _screenPosition;
        /// <summary>
        /// Canvas of the communication screen
        /// </summary>
        private Canvas _comScreenCanvas;


        private void Start()
        {
            _screenPosition = GameObject.FindGameObjectWithTag("IntroScreen");
            _dock = FindObjectOfType<CubeDock>();
            _comScreenCanvas = GameObject.FindGameObjectWithTag("ComScreen").GetComponent<Canvas>();
            _comScreenCanvas.enabled = false;
            IntroVideo();
        }

        private void Update()
        {
            //Did the video stopped playing?
            if (_screenInstance != null && !_screenInstance.GetComponent<VideoPlayer>().isPlaying)
            {
                IntroVideoStopped();
            }

            //Which face of the holocube has been docked?
            if (_cubeInitializable && _dock.face == "station")
            {
                ActivatingHolocube();
            }
            else if (_comInitializable && _dock.face == "tubex")
            {
                ActivationCommunicationScreen();
            }
        }

        /// <summary>
        /// Intro video that explains the universe of the game.
        /// </summary>
        private void IntroVideo()
        {
            _screenInstance = (GameObject)Instantiate(_screenPrefab, _screenPosition.transform.position, _screenPosition.transform.rotation);
        }

        /// <summary>
        /// Called when intro video has stopped playing
        /// </summary>
        private void IntroVideoStopped()
        {
            Destroy(_screenInstance);
            _cubeInitializable = true;
        }

        /// <summary>
        /// Called when holocube is placed on station face
        /// </summary>
        private void ActivatingHolocube()
        {
            _comInitializable = true;
        }

        /// <summary>
        /// Initialization of the communication screen.
        /// </summary>
        private void ActivationCommunicationScreen()
        {
            _comScreenCanvas.enabled = true;
            tabletScreen.StartLaunch();
            _maintenanceLaunchable = true;
        }

        /// <summary>
        /// Called when launch button pressed
        /// </summary>
        public void OnLaunchSuccess()
        {
            topScreen.StartMaintenance();
            hologram.gameObject.SetActive(true);
        }

        /// <summary>
        /// Called when first hologram cleared successfully
        /// </summary>
        public void OnIrregularitiesSuccess()
        {
            topScreen.ContinueMaintenance();
            tabletScreen.StartSecondMaintenance();
        }

        /// <summary>
        /// Player has to remove the irregularities a second time with an impossible level.
        /// </summary>
        public void MaintenanceVirus()
        {
            hologram.gameObject.SetActive(false);
            hologramSecond.gameObject.SetActive(true);
            _isEnd = true;
        }

        /// <summary>
        /// Fails the maintenance and launches the game phase.
        /// </summary>
        public void EndMaintenance()
        {
            Debug.Log("Maintenance failed");
        }

        protected override void PreShow(VirtualWallTopZone wallTopZone, ElementInfo[] info)
        {
            base.PreShow(wallTopZone, info);
            tabletScreen = GetElement<TutorialTabletScreen>();
            topScreen = GetElement<TutorialTopScreen>();
            tubeScreen = GetElement<TutorialTubeScreen>();
        }

        protected override void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, int randomSeed, XPState stateOnActivation)
        {
            base.PostInit(xpContext, info, logController, randomSeed, stateOnActivation);
            hologram = GetElement<TutorialHologram>();
            hologramSecond = GetElement<TutorialHologramVirus>();
        }
    }
}
