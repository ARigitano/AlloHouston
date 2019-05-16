using CRI.HelloHouston.GameElement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System;

namespace CRI.HelloHouston.Experience
{
    /// <summary>
    /// Hologram that changes the experiment tubes.
    /// </summary>
    public class ChangingTube : MonoBehaviour, IHologram
    {
        /// <summary>
        /// Is the experiment currently loading?
        /// </summary>
        public bool isLoading = false;
        /// <summary>
        /// List of all the available experiments for this game
        /// </summary>
        public List<ErrorTubeX> tubes;

        public bool visible
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(3.0f);
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
                tube.SetUnavailable();
            }
            StartCoroutine(Wait());
        }

        /// <summary>
        /// Called when a tube has finished loading.
        /// </summary>
        private void LoadingFinished()
        {
            foreach (ErrorTubeX tube in tubes)
            {
                tube.SetAvailable();
            }
        }

        public void ShowHologram()
        {
            throw new NotImplementedException();
        }

        public void HideHologram()
        {
            throw new NotImplementedException();
        }
    }
}
