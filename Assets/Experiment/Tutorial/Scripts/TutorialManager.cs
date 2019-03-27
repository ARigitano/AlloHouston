using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Start()
        {
            
        }

        private void IntroVideo()
        {
            //disables every layers in camera
            //instantiates screens
            //plays intro video
            //when video ends fade in black post processing 
            //destroys screen
            //makes layers visible again
            //fades out
        }


    }
}
