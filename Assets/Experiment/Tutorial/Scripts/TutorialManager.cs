using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.GameElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// The XPManager for the tutorial experiment.
/// </summary>
namespace CRI.HelloHouston.Experience.Tutorial
{


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
        /// The hologram tube of the table block.
        /// </summary>
        public TutorialHologram hologram { get; private set; }
        /// <summary>
        /// The hologram tube of the table block.
        /// </summary>
        public TutorialHologramVirus hologramSecond { get; private set; }
        /// <summary>
        /// Settings of the experience.
        /// </summary>
        public TutorialSettings settings { get; private set; }
        private GameObject _player;
        [SerializeField]
        private VideoPlayer _introVideo;
        private bool _cubeInitializable = false;
        private bool _comInitializable = false;
        private bool _maintenanceLaunchable = false;
        private bool _maintenanceStartable = false;
        private bool _firstMaintenance = false;
        private bool _isEnd = false;
        [SerializeField]
        private GameObject _screenPrefab;
        private GameObject _screenInstance;
        private CubeDock _dock;
        private bool _isDestroyed = false;
        private GameObject _screenPosition;
        private Canvas _comScreenCanvas;


        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("MainCamera");
            _screenPosition = GameObject.FindGameObjectWithTag("IntroScreen");
            _dock = FindObjectOfType<CubeDock>();
            IntroVideo();
        }

        private void Update()
        {
            //Did the video stopped playing?
            if (_screenInstance != null && !_screenInstance.GetComponent<VideoPlayer>().isPlaying)
            {
                IntroVideoStopped();
            }

            //Has the player removed all irregularities?
            if(_maintenanceStartable)
            {

            }
        }

        /// <summary>
        /// Intro video that explains the universe of the game.
        /// </summary>
        private void IntroVideo()
        {
            //disables every layers in camera
            _screenInstance = (GameObject)Instantiate(_screenPrefab, _screenPosition.transform.position, _screenPosition.transform.rotation);
            //_introVideo.loopPointReached += EndReached;
        }

        /// <summary>
        /// Called when intro video has stopped playing
        /// </summary>
        private void IntroVideoStopped()
        {
            Destroy(_screenInstance);

            _cubeInitializable = true;
            Debug.Log("Intro video stopped");
        }

        /// <summary>
        /// Called when holocube is placed on station face
        /// </summary>
        private void ActivatingHolocube()
        {
            //REQUIRES HOLOCUBE
            //launched if holocube has been placed on table on station map face
            //launches initialization of holocube
            //checks if holocube has been initialized
            //sets next step
            _comInitializable = true;
        }

        /// <summary>
        /// Initialization of the communication screen.
        /// </summary>
        private void ActivationCommunicationScreen()
        {
            //REQUIRES HOLOCUBE
            //REQUIRES COMMUNICATION SCREEN
            //launched if holocube has been placed on table on tubex selection face
            //launches initialization of communication screen
            //checks if communication screen is initiaized
            //checks if maintenance tubex has been loaded
            //sets next step
            _comScreenCanvas.enabled = true;
            tabletScreen.StartLaunch();
            _maintenanceLaunchable = true;
            Debug.Log("Com screen activated");
        }

        /// <summary>
        /// Called when launch button pressed
        /// </summary>
        public void OnLaunchSuccess()
        {
            //stepManager.SkipToStep("Maintenance");
            topScreen.StartMaintenance();
            hologram.gameObject.SetActive(true);
            //MaintenanceStarted();
        }

        /// <summary>
        /// Called when first hologram cleared successfully
        /// </summary>
        public void OnIrregularitiesSuccess()
        {
            //stepManager.SkipToStep("Irregularities");
            topScreen.ContinueMaintenance();
            tabletScreen.StartSecondMaintenance();
        }

        /// <summary>
        /// Player has to remove the irregularities a second time with an impossible level.
        /// </summary>
        public void MaintenanceVirus()
        {
            //REQUIRES HOLOCUBE
            //MODIF HOLOGRAM
            //launched when first step of maintenance has been completed
            //checks end of timer
            //sets next step
            hologram.gameObject.SetActive(false);
            hologramSecond.gameObject.SetActive(true);
            _isEnd = true;
        }

        /// <summary>
        /// Fails the maintenance and launches the game phase.
        /// </summary>
        public void EndMaintenance()
        {
            //launched if first or second steps of maintenance have been failed
            //produces the action leading to beginnning of game
            Debug.Log("Maintenance failed");
        }

        protected override void PreShow(VirtualWallTopZone wallTopZone, VirtualHologramZone hologramZone, ElementInfo[] info)
        {
            base.PreShow(wallTopZone, hologramZone, info);
            tabletScreen = GetElement<TutorialTabletScreen>();
            topScreen = GetElement<TutorialTopScreen>();
            tubeScreen = GetElement<TutorialTubeScreen>();
        }

        protected override void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, int randomSeed, XPVisibility visibilityOnActivation)
        {
            base.PostInit(xpContext, info, logController, randomSeed, visibilityOnActivation);
            hologram = GetElement<TutorialHologram>();
            hologramSecond = GetElement<TutorialHologramVirus>();
        }
    }
}
