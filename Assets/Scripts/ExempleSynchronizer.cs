using CRI.HelloHouston.Calibration;
using CRI.HelloHouston.Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The synchronizer of the particle physics experiment.
/// </summary>
namespace CRI.HelloHouston.ParticlePhysics
{
    public class ExempleSynchronizer : XPSynchronizer
    {
        /// <summary>
        /// The top left script of the experiment block.
        /// </summary>
        private FakeTopScreen _fakeTopScreen;
        /// <summary>
        /// The top right script of the experiment block.
        /// </summary>
        private FakeTubeScreen _fakeTubeScreen;
        /// <summary>
        /// The tablet script of the experiment block.
        /// </summary>
        private FakeTabletScreen _fakeTabletScreen;
        /// <summary>
        /// The hologram scripts of the table block.
        /// </summary>
        private FakeHologram[] _holograms;

        public void LoadingBarFinished()
        {
            if (state == XPState.Visible)
                _fakeTabletScreen.WaitingConfirmation();
        }

        public void StartButtonClicked()
        {
            if (state == XPState.Visible)
                _fakeTopScreen.ManualOverride();
        }

        public void OverrideButtonClicked()
        {
            if (state == XPState.Visible)
                _fakeTopScreen.AccessCode();
        }

        public void CorrectPassword()
        {
            if (state == XPState.Visible)
                _fakeTopScreen.Access(true);
        }

        public void IncorrectPassword()
        {
            if (state == XPState.Visible)
                _fakeTopScreen.Access(false);
        }

        public void EnteringDigit()
        {
            if (state == XPState.Visible)
                _fakeTopScreen.DisplayPassword(_fakeTabletScreen.enteredPassword);
        }

        public void AccessGranted()
        {
            if (state == XPState.Visible)
                _fakeTabletScreen.AccessGranted();
        }

        public void EnteringParticles()
        {
            if (state == XPState.Visible)
                _fakeTopScreen.DisplayParticles(_fakeTabletScreen._enteredParticles);
        }

        public void CorrectParticle()
        {
            if (state == XPState.Visible)
            {
                _fakeTopScreen.CorrectParticle();
                _holograms[0].AnimHologram(_fakeTabletScreen.reactionExits);
            }
        }

        public void IncorrectParticle()
        {
            if (state == XPState.Visible)
                _fakeTopScreen.IncorrectParticle();
        }
        
        protected override void PostActivate()
        {
            _holograms[0].AnimHologram(_fakeTabletScreen.reactionExits);
        }

        protected override void PreShow(VirtualWallTopZone wallTopZone, ElementInfo[] info)
        {
            base.PreShow(wallTopZone, info);
            _fakeTabletScreen = GetElement<FakeTabletScreen>();
            _fakeTopScreen = GetElement<FakeTopScreen>();
            _fakeTubeScreen = GetElement<FakeTubeScreen>();
        }

        protected override void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, XPState stateOnActivation)
        {
            base.PostInit(xpContext, info, logController, stateOnActivation);
            _holograms = GetElements<FakeHologram>();
            _holograms[0].Init(this);
            _fakeTabletScreen = GetElement<FakeTabletScreen>();
            _fakeTopScreen = GetElement<FakeTopScreen>();
            _fakeTopScreen.Init(this);
            _fakeTubeScreen = GetElement<FakeTubeScreen>();
        }
    }
}
