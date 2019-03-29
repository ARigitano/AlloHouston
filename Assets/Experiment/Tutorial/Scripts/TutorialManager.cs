using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private Camera _player;

        /// <summary>
        /// Intro video that explains the universe of the game.
        /// </summary>
        private void IntroVideo()
        {
            //disables every layers in camera
            //instantiates screen
            //plays intro video
            //when video ends fade in black post processing 
            //destroys screen
            //makes layers visible again
            //fades out
        }

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
        }

        /// <summary>
        /// Launched when first button of the tablet has been pressed.
        /// </summary>
        private void MaintenanceStarted()
        {
            //MODIF TABLET
            //launched if the "launch now" button of the tablet has been pressed
            //sets next step
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
