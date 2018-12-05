using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The synchronizer of the particle physics experiment.
/// </summary>
namespace CRI.HelloHouston.ParticlePhysics
{
    public class FakeSynchronizer : MonoBehaviour
    {
        /// <summary>
        /// The top left script of the experiment block.
        /// </summary>
        [SerializeField]
        private FakeTopScreen _fakeTopScreen;
        /// <summary>
        /// The top right script of the experiment block.
        /// </summary>
        [SerializeField]
        private FakeTubeScreen _fakeTubeScreen;
        /// <summary>
        /// The tablet script of the experiment block.
        /// </summary>
        [SerializeField]
        private FakeTabletScreen _fakeTabletScreen;
        /// <summary>
        /// The bottom scripts of the experiment block.
        /// </summary>
        [SerializeField]
        private FakeBottomElement[] _fakeBottomElements;
        /// <summary>
        /// The hologram scripts of the table block.
        /// </summary>
        [SerializeField]
        private FakeHologram[] _fakeHolograms;
        /// <summary>
        /// The corner scripts of the corner block.
        /// </summary>
        [SerializeField]
        private FakeCornerScreen[] _fakeCornerScreens;
        /// <summary>
        /// The door script of the door block.
        /// </summary>
        [SerializeField]
        private FakeDoor _fakeDoor;

        /// <summary>
        /// Effect when the experiment is activated the first time.
        /// </summary>
        private void Launched()
        {
            if(_fakeTopScreen != null) _fakeTopScreen.OnActivated();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnActivated();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnActivated();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if(fakeBottomElement != null) fakeBottomElement.OnActivated();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnActivated();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnActivated();

            if (_fakeDoor != null) _fakeDoor.OnActivated();
        }

        /// <summary>
        /// Synchronizes all the blocks of the experiment when a block sends data.
        /// </summary>
        /// <param name="state"></param>
        public void SynchronizeScreens(string state)
        {
            switch(state)
            {
                case "loadingBarFinished":
                    _fakeTabletScreen.WaitingConfirmation();
                    break;
                case "StartButtonClicked":
                    _fakeTopScreen.ManualOverride();
                    break;
                case "OverrideButtonClicked":
                    _fakeTopScreen.AccessCode();
                    break;
                case "PasswordCorrect":
                    _fakeTopScreen.Access(true);
                    break;
                case "PasswordInCorrect":
                    _fakeTopScreen.Access(false);
                    break;
                case "EnteringDigit":
                    _fakeTopScreen.DisplayPassword(_fakeTabletScreen.enteredPassword);
                    break;
                case "AccessGranted":
                    _fakeTabletScreen.AccessGranted();
                    break;
                case "EnteringParticle":
                    _fakeTopScreen.DisplayParticles(_fakeTabletScreen._enteredParticles);
                    break;
                case "ParticleCorrect":
                    _fakeTopScreen.CorrectParticle();
                    break;
                case "ParticleInCorrect":
                    _fakeTopScreen.IncorrectParticle();
                    _fakeHolograms[0].AnimHologram(_fakeTabletScreen.particleTypes);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Effect when the experiment is paused.
        /// </summary>
        private void Paused()
        {
            if (_fakeTopScreen != null) _fakeTopScreen.OnPause();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnPause();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnPause();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if (fakeBottomElement != null) fakeBottomElement.OnPause();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnPause();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnPause();

            if (_fakeDoor != null) _fakeDoor.OnPause();
        }

        /// <summary>
        /// Effect when the experiment is unpaused.
        /// </summary>
        private void UnPaused()
        {
            if (_fakeTopScreen != null) _fakeTopScreen.OnUnpause();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnUnpause();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnUnpause();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if (fakeBottomElement != null) fakeBottomElement.OnUnpause();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnUnpause();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnUnpause();

            if (_fakeDoor != null) _fakeDoor.OnUnpause();
        }

        /// <summary>
        /// Launches an action related to the experiment.
        /// </summary>
        private void LaunchAction()
        {
            
        }

        /// <summary>
        /// Effect when the experiment is correctly resolved.
        /// </summary>
        private void Resolved()
        {
            if (_fakeTopScreen != null) _fakeTopScreen.OnResolved();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnResolved();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnResolved();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if (fakeBottomElement != null) fakeBottomElement.OnResolved();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnResolved();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnResolved();

            if (_fakeDoor != null) _fakeDoor.OnResolved();
        }

        /// <summary>
        /// Effect when the experiment is failed.
        /// </summary>
        private void Failed()
        {
            if (_fakeTopScreen != null) _fakeTopScreen.OnFailed();
            if (_fakeTubeScreen != null) _fakeTubeScreen.OnFailed();
            if (_fakeTabletScreen != null) _fakeTabletScreen.OnFailed();

            foreach (FakeBottomElement fakeBottomElement in _fakeBottomElements)
                if (fakeBottomElement != null) fakeBottomElement.OnFailed();

            foreach (FakeHologram fakeHologram in _fakeHolograms)
                if (fakeHologram != null) fakeHologram.OnFailed();

            foreach (FakeCornerScreen fakeCornerScreen in _fakeCornerScreens)
                if (fakeCornerScreen != null) fakeCornerScreen.OnFailed();

            if (_fakeDoor != null) _fakeDoor.OnFailed();
        }

        // Use this for initialization
        void Start()
        {
            Launched();
            _fakeHolograms[0].AnimHologram(_fakeTabletScreen.particleTypes);
        }
    }
}
