using CRI.HelloHouston.Calibration;

/// <summary>
/// The synchronizer of the particle physics experiment.
/// </summary>
namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIASynchronizer : XPSynchronizer
    {
        /// <summary>
        /// The top left script of the experiment block.
        /// </summary>
        private MAIATopScreen _fakeTopScreen;
        /// <summary>
        /// The top right script of the experiment block.
        /// </summary>
        private MAIATubeScreen _fakeTubeScreen;
        /// <summary>
        /// The tablet script of the experiment block.
        /// </summary>
        private MAIATabletScreen _fakeTabletScreen;
        /// <summary>
        /// The hologram scripts of the table block.
        /// </summary>
        private MAIAHologram[] _holograms;

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
            _fakeTabletScreen = GetElement<MAIATabletScreen>();
            _fakeTopScreen = GetElement<MAIATopScreen>();
            _fakeTubeScreen = GetElement<MAIATubeScreen>();
        }

        protected override void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, XPState stateOnActivation)
        {
            base.PostInit(xpContext, info, logController, stateOnActivation);
            _holograms = GetElements<MAIAHologram>();
            _holograms[0].Init(this);
            _fakeTabletScreen = GetElement<MAIATabletScreen>();
            _fakeTopScreen = GetElement<MAIATopScreen>();
            _fakeTopScreen.Init(this);
            _fakeTubeScreen = GetElement<MAIATubeScreen>();
        }
    }
}
