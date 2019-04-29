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

            //Which face of the holocube has been docked?
            if (_cubeInitializable && _dock.face == "station")
            {
                ActivatingHolocube();
            }
            else if (_comInitializable && _dock.face == "tubex")
            {
                ActivationCommunicationScreen();
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

        private void IntroVideoStopped()
        {
            Destroy(_screenInstance);

            _cubeInitializable = true;
            Debug.Log("Intro video stopped");
        }

        /*void EndReached(UnityEngine.Video.VideoPlayer vp)
        {
            //when video ends fade in black post processing 
            //destroys screen
            //makes layers visible again
            //fades out
            _cubeInitializable = true;
        }*/

        /// <summary>
        /// Initialization of the holocube.
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
            tabletScreen.StartLaunch();
            _maintenanceLaunchable = true;
        }

        public void OnLaunchSuccess()
        {
            //stepManager.SkipToStep("Maintenance");
            topScreen.StartMaintenance();
            hologram.gameObject.SetActive(true);
            MaintenanceStarted();
        }

        public void OnIrregularitiesSuccess()
        {
            //stepManager.SkipToStep("Irregularities");
            topScreen.ContinueMaintenance();
            hologram.gameObject.SetActive(true);
            Maintenance();
        }

        /// <summary>
        /// Launched when first button of the tablet has been pressed.
        /// </summary>
        private void MaintenanceStarted()
        {
            //MODIF TABLET
            //launched if the "launch now" button of the tablet has been pressed
            //sets next step
            _maintenanceStartable = true;
        }

        /// <summary>
        /// Player has to remove the irregularities a first time with an easy level.
        /// </summary>
        private void Maintenance()
        {
            //REQUIRES HOLOCUBE
            //MODIF HOLOGRAM
            //launched if maintenance hologram has been cleared from irregularities or not during timer
            //sets next step
            _firstMaintenance = true;
        }

        /// <summary>
        /// Player has to remove the irregularities a second time with an impossible level.
        /// </summary>
        private void MaintenanceVirus()
        {
            //REQUIRES HOLOCUBE
            //MODIF HOLOGRAM
            //launched when first step of maintenance has been completed
            //checks end of timer
            //sets next step
            _isEnd = true;
        }

        /// <summary>
        /// Fails the maintenance and launches the game phase.
        /// </summary>
        private void EndMaintenance()
        {
            //launched if first or second steps of maintenance have been failed
            //produces the action leading to beginnning of game
        }


    }
}
