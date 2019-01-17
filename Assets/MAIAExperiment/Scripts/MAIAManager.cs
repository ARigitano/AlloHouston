using CRI.HelloHouston.Calibration;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The synchronizer of the particle physics experiment.
/// </summary>
namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAManager : XPManager
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




        /// <summary>
        /// All the particle scriptable objects.
        /// </summary>
        private Particle[] _allParticles;
        /// <summary>
        /// All the reaction scriptable objects.
        /// </summary>
        [HideInInspector]
        public Reaction[] _allReactions;
        /// <summary>
        /// Path to the particle scriptable objects folder.
        /// </summary>
        private static string _path = "Particles";
        /// <summary>
        /// Path to the particle scriptable objects folder.
        /// </summary>
        private static string _pathReaction = "reactions";
        /// <summary>
        /// Contains the combination of particles randomly generated.
        /// </summary>
        [HideInInspector]
        public Particle[] particleTypes;
        /// <summary>
        /// Real password to get access.
        /// </summary>
        [SerializeField]
        private string _realPassword;
        /// <summary>
        /// Password entered by the player.
        /// </summary>
        [HideInInspector]
        public string enteredPassword;
        /// <summary>
        /// The combination of particles randomly generated rewritten as a string.
        /// </summary>
        [HideInInspector]
        public List<string> realParticles = new List<string>();
        /// <summary>
        /// The particles entered by the player.
        /// </summary>
        [HideInInspector]
        public List<Particle> _enteredParticles = new List<Particle>();
        /// <summary>
        /// String displayed depending on the particles combination entered.
        /// </summary>
        [HideInInspector]
        public string[] result;
        /// <summary>
        /// Number of ongoing reactions.
        /// </summary>
        [SerializeField]
        private int _numberChosenReaction = 4;
        /// <summary>
        /// The ongoing reactions.
        /// </summary>
        [HideInInspector]
        public List<Reaction> _chosenReactions = new List<Reaction>();
        /// <summary>
        /// The reaction to idetify.
        /// </summary>
        [HideInInspector]
        public Reaction _realReaction;
        /// <summary>
        /// The particles produced by the ongoing reactions.
        /// </summary>
        [HideInInspector]
        public List<Particle> reactionExits = new List<Particle>();
        /// <summary>
        /// Index of the Feynman diagram currently being displayed.
        /// </summary>
        [HideInInspector]
        public int displayedDiagram = 0;
        /// <summary>
        /// An error depending on the payer's diagram selection mistake.
        /// </summary>
        [HideInInspector]
        public string particleErrorString;
        /// <summary>
        /// The numbers of particle detected of each kind.
        /// </summary>
        [HideInInspector]
        public int nbQuark, nbAntiquark, nbMuon, nbAntimuon, nbElectron, nbAntielectron, nbNeutrino, nbPhoton;

        /// <summary>
        /// Activates the manual override panel of the tablet.
        /// </summary>
        public void ManualOverrideActive()
        {
            _tabletScreen.ManualOverride();
        }

        /// <summary>
        /// Directly skips to the Feynman diagrams step.
        /// </summary>
        public void SkipStepOne()
        {
            _holograms[0].ActivateHologram(true);
            _tabletScreen.SkipStepOne();
            _tubeScreen.SkipStepOne();
            _topScreen.SkipStepOne();
        }

        /// <summary>
        /// Tells the tablet that the experiment has finished loading.
        /// </summary>
        public void LoadingBarFinished()
        {
            _tabletScreen.WaitingConfirmation();
        }

        /// <summary>
        /// Tells the main screen that the start button has been pressed.
        /// </summary>
        public void StartButtonClicked()
        {
            _topScreen.ManualOverride();
        }

        /// <summary>
        /// Tells the main screen that the override button has been pressed.
        /// </summary>
        public void OverrideButtonClicked()
        {
            _topScreen.AccessCode();
        }

        /// <summary>
        /// Tells the main screen that the right password has been entered.
        /// </summary>
        public void CorrectPassword()
        {
            //rewrite
            //_topScreen.Access(true);
        }

        /// <summary>
        /// Tells the main screen that an incorrect password has been entered.
        /// </summary>
        public void IncorrectPassword()
        {
            //TODO:rewrite
            //_topScreen.Access(false);
        }

        /// <summary>
        /// Tells the main screen that a password digit has been entered.
        /// </summary>
        public void EnteringDigit()
        {
            //TODO:rewrite
            //_topScreen.DisplayPassword(_tabletScreen.enteredPassword);
        }

        /// <summary>
        /// Tells the tablet that access to the experiment has been granted.
        /// </summary>
        public void AccessGranted()
        {
            //TODO: rewrite
            _holograms[0].ActivateHologram(true);
            _tabletScreen.AccessGranted();
            //_tabletScreen.reactionExits = _tabletScreen.ParticlesCombination();
            //_topScreen.FillNbParticlesDetected(_tabletScreen.reactionExits);
        }

        /// <summary>
        /// Tells the main screen that a particle has been entered.
        /// </summary>
        public void EnteringParticles()
        {
            //TODO: rewrite
            //_topScreen.DisplayParticles(_tabletScreen._enteredParticles);
        }

        /// <summary>
        /// Tells the main screen that the correct combination of particles has been entered.
        /// </summary>
        public void CorrectParticle()
        {
            //TODO: rewrite
            //_holograms[0].AnimHologram(_tabletScreen.reactionExits);
            _holograms[0].DisplaySplines();
            /*_topScreen.ParticleGrid(_tabletScreen.reactionExits);
            _topScreen.FillParticlesTable(_tabletScreen.nbAntielectron, _topScreen._textAntielectron);
            _topScreen.FillParticlesTable(_tabletScreen.nbAntimuon, _topScreen._textAntimuon);
            _topScreen.FillParticlesTable(_tabletScreen.nbAntiquark, _topScreen._textAntiquark);
            _topScreen.FillParticlesTable(_tabletScreen.nbElectron, _topScreen._textElectron);
            _topScreen.FillParticlesTable(_tabletScreen.nbMuon, _topScreen._textMuon);
            _topScreen.FillParticlesTable(_tabletScreen.nbNeutrino, _topScreen._textNeutrino);
            _topScreen.FillParticlesTable(_tabletScreen.nbPhoton, _topScreen._textPhoton);
            _topScreen.FillParticlesTable(_tabletScreen.nbQuark, _topScreen._textQuark);
            _topScreen.FillChosenDiagrams(_tabletScreen._chosenReactions, _tabletScreen._realReaction);
            _topScreen.FillInteractionType(_tabletScreen._realReaction);*/
        }

        /// <summary>
        /// Tells the main screen to clear all the entered particles.
        /// </summary>
        public void ClearParticles()
        {
            _topScreen.ClearParticles();
        }

        /// <summary>
        /// Tells the main screen to clear the last entered particles.
        /// </summary>
        public void DeleteParticle()
        {
            //TODO:rewrite
            //_topScreen.DeleteParticle(_tabletScreen._enteredParticles.Count);
        }

        /// <summary>
        /// Tells the tube screen to display another Feynman diagram.
        /// </summary>
        public void OtherDiagram()
        {
            //TODO:rewrite
            //_tubeScreen.OtherDiagram(_tabletScreen.displayedDiagram, _tabletScreen._allReactions);
        }

        /// <summary>
        /// Tells the tube screen to mark a Feynman diagram for its exits.
        /// </summary>
        public void SelectExit()
        {
            //TODO:rewrite
            //_tubeScreen.SelectExit(_tabletScreen.displayedDiagram);
        }

        /// <summary>
        /// Tells the tube screen to mark a Feynman diagram for its interactions.
        /// </summary>
        public void SelectInteraction()
        {
            //TODO:rewrite
            //_tubeScreen.SelectInteraction(_tabletScreen.displayedDiagram);
        }

        /// <summary>
        /// Tells the top screen that a combination of particles with a wrong length has been entered.
        /// </summary>
        public void ParticleWrongLength()
        {
            //TODO:rewrite
            //_topScreen.ErrorParticles(_tabletScreen.particleErrorString);
        }

        /// <summary>
        /// Tells the top screen that a combination of particles with the wrong symbols has been entered.
        /// </summary>
        public void ParticleWrongSymbol()
        {
            //TODO:rewrite
            //_topScreen.ErrorParticles(_tabletScreen.particleErrorString);
        }

        /// <summary>
        /// Tells the top screen that a combination of particles with the wrong charges has been entered.
        /// </summary>
        public void ParticleWrongCharge()
        {
            //TODO:rewrite
            //_topScreen.ErrorParticles(_tabletScreen.particleErrorString);
        }

        /// <summary>
        /// Tells every screen that the right combination of particles has been entered.
        /// </summary>
        public void ParticleRightCombination()
        {
            _topScreen.OverrideSecond();
            _tabletScreen.OverrideSecond();
            //Disabled for the demo version
           //_tubeScreen.OverrideSecond(_tabletScreen._allReactions);
        }

        /// <summary>
        /// Tells the main screen that a reaction has been selected.
        /// </summary>
        public void ReactionSelected()
        {
            //TODO:rewrite
            //_topScreen.ReactionSelected(_tabletScreen._realReaction, _tubeScreen.diagramSelected);
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
            _tabletScreen = GetElement<MAIATabletScreen>();
            _topScreen = GetElement<MAIATopScreen>();
            _tubeScreen = GetElement<MAIATubeScreen>();
        }
    }
}
