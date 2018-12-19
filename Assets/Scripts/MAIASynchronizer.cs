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
        private MAIATopScreen _topScreen;
        /// <summary>
        /// The top right script of the experiment block.
        /// </summary>
        private MAIATubeScreen _tubeScreen;
        /// <summary>
        /// The tablet script of the experiment block.
        /// </summary>
        private MAIATabletScreen _tabletScreen;
        /// <summary>
        /// The hologram scripts of the table block.
        /// </summary>
        private MAIAHologram[] _holograms;

        public void LoadingBarFinished()
        {
            _tabletScreen.WaitingConfirmation();
        }

        public void StartButtonClicked()
        {
            _topScreen.ManualOverride();
        }

        public void OverrideButtonClicked()
        {
            _topScreen.AccessCode();
        }

        public void CorrectPassword()
        {
            _topScreen.Access(true);
        }

        public void IncorrectPassword()
        {
            _topScreen.Access(false);
        }

        public void EnteringDigit()
        {
            _topScreen.DisplayPassword(_tabletScreen.enteredPassword);
        }

        public void AccessGranted()
        {
            _tabletScreen.AccessGranted();
            _tabletScreen.reactionExits = _tabletScreen.ParticlesCombination();
            _topScreen.FillNbParticlesDetected(_tabletScreen.reactionExits);
        }

        public void EnteringParticles()
        {
            _topScreen.DisplayParticles(_tabletScreen._enteredParticles);
        }

        public void CorrectParticle()
        {
            _holograms[0].AnimHologram(_tabletScreen.reactionExits);
            _topScreen.ParticleGrid(_tabletScreen.reactionExits);
            _topScreen.FillParticlesTable(_tabletScreen.reactionExits);
            _topScreen.FillChosenDiagrams(_tabletScreen._chosenReactions, _tabletScreen._realReaction);
            _topScreen.FillInteractionType(_tabletScreen._realReaction);
        }

        public void ClearParticles()
        {
            _topScreen.ClearParticles();
        }

        public void OtherDiagram()
        {
            _tubeScreen.OtherDiagram(_tabletScreen.displayedDiagram, _tabletScreen._allReactions);
        }

        public void SelectExit()
        {
            _tubeScreen.SelectExit(_tabletScreen.displayedDiagram);
        }

        public void SelectInteraction()
        {
            _tubeScreen.SelectInteraction(_tabletScreen.displayedDiagram);
        }

        public void ParticleWrongLength()
        {
            _topScreen.ErrorParticles(_tabletScreen.particleErrorString);
        }

        public void ParticleWrongSymbol()
        {
            _topScreen.ErrorParticles(_tabletScreen.particleErrorString);
        }

        public void ParticleWrongCharge()
        {
            _topScreen.ErrorParticles(_tabletScreen.particleErrorString);
        }

        public void ParticleRightCombination()
        {
            _topScreen.OverrideSecond();
            _tabletScreen.OverrideSecond();
            _tubeScreen.OverrideSecond(_tabletScreen._allReactions);
        }

        public void ReactionSelected()
        {
            _topScreen.ReactionSelected(_tabletScreen._realReaction, _tubeScreen.diagramSelected);
        }

        protected override void PreShow(VirtualWallTopZone wallTopZone, ElementInfo[] info)
        {
            base.PreShow(wallTopZone, info);
            _tabletScreen = GetElement<MAIATabletScreen>();
            _topScreen = GetElement<MAIATopScreen>();
            _tubeScreen = GetElement<MAIATubeScreen>();
        }

        protected override void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, XPState stateOnActivation)
        {
            base.PostInit(xpContext, info, logController, stateOnActivation);
            _holograms = GetElements<MAIAHologram>();
            _holograms[0].Init(this);
            _tabletScreen = GetElement<MAIATabletScreen>();
            _tabletScreen.Init(this);
            _topScreen = GetElement<MAIATopScreen>();
            _topScreen.Init(this);
            _tubeScreen = GetElement<MAIATubeScreen>();
            _tubeScreen.Init(this);
        }
    }
}
