using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Hologram that changes the experiment tubes.
    /// </summary>
    public class ChangingTube : XPHologramElement
    {
        /// <summary>
        /// Is the experiment currently loading?
        /// </summary>
        public bool isLoading = false;
        /// <summary>
        /// List of all the available experiments for this game
        /// </summary>
        public List<ErrorTubeX> tubes;

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3f);
            LoadingFinished();
        }

        /// <summary>
        /// Called when a tube is loading.
        /// </summary>
        /// <param name="experience">The experiment loading</param>
        /// <param name="topZone">The wall top zone on which the experiment is loading.</param>
        public void LoadingTube(XPManager experience, XPWallTopZone topZone)
        {
            foreach(ErrorTubeX tube in tubes)
            {
                tube.IsNotAvailable();
            }
            StartCoroutine("Wait");
        }

        /// <summary>
        /// Called when a tube has finished loading.
        /// </summary>
        private void LoadingFinished()
        {
            foreach (ErrorTubeX tube in tubes)
            {
                tube.IsAvailable();
            }
        }
    }
}
