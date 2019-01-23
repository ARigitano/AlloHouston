using CRI.HelloHouston.Calibration;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
        /// Settings of the experience.
        /// </summary>
        public MAIASettings settings { get; private set; }
        /// <summary>
        /// The ongoing reactions.
        /// </summary>
        public List<Reaction> ongoingReactions { get; private set; }
        /// <summary>
        /// The reaction to idetify.
        /// </summary>
        public Reaction selectedReaction { get; private set; }
        /// <summary>
        /// The particles produced by the ongoing reactions.
        /// </summary>
        public List<Particle> producedParticles { get; private set; }

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
            if (producedParticles.Count == 0)
                GenerateParticles();
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
            _topScreen.Access(true);
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

        /// <summary>
        /// Selects the ongoing particle reactions for this game.
        /// </summary>
        private List<Reaction> SelectReactions()
        {
            ongoingReactions = settings.allReactions
                .Where(reaction => reaction.fundamental)
                .OrderBy(reaction => Random.value)
                .Take(settings.reactionCount)
                .ToList();
            selectedReaction = ongoingReactions[Random.Range(0, settings.reactionCount)];
            logController.AddLog(selectedReaction.name, xpContext);
            return ongoingReactions;
        }

        /// <summary>
        /// Counts the number of particles detetected of each kind.
        /// </summary>
        /// <param name="particles">The particles detected.</param>
        private void DisplayParticles(List<Particle> particles)
        {
            foreach (var particleGroup in particles.GroupBy(particle => particle.particleName))
                logController.AddLog(string.Format("{0}: {1}", particleGroup.Key, particleGroup.Count()), xpContext, Log.LogType.Default);
        }

        private List<Particle> GenerateParticles()
        {
            List<Reaction> currentReactions = SelectReactions();
            producedParticles = currentReactions.SelectMany(reaction => reaction.exit.particles).ToList();
            DisplayParticles(producedParticles);
            return producedParticles;
        }

        protected override void PreShow(VirtualWallTopZone wallTopZone, ElementInfo[] zones)
        {
            base.PreShow(wallTopZone, zones);
            _tabletScreen = GetElement<MAIATabletScreen>();
            _topScreen = GetElement<MAIATopScreen>();
            _tubeScreen = GetElement<MAIATubeScreen>();
        }

        protected override void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, XPState stateOnActivation)
        {
            base.PostInit(xpContext, info, logController, stateOnActivation);
            _holograms = GetElements<MAIAHologram>();
            settings = (MAIASettings)xpContext.xpSettings;
            List<Particle> particle = GenerateParticles();
            _holograms[0].CreateSplines(particle);
        }
    }
}
