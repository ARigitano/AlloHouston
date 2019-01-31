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
        public MAIATopScreen topScreen { get; private set; }
        /// <summary>
        /// The top right script of the experiment block.
        /// </summary>
        public MAIATubeScreen tubeScreen { get; private set; }
        /// <summary>
        /// The tablet script of the experiment block.
        /// </summary>
        public MAIATabletScreen tabletScreen { get; private set; }
        /// <summary>
        /// The hologram scripts of the table block.
        /// </summary>
        public MAIAHologram[] holograms;
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
        public List<Particle> generatedParticles { get; private set; }

        #region GameMasterActions

        /// <summary>
        /// Selects the ongoing particle reactions for this game.
        /// </summary>
        public void SkipStepOne()
        {
            if (generatedParticles.Count == 0)
                GenerateParticles();
            holograms[0].ActivateHologram(false);
            tabletScreen.SkipStepOne();
            tubeScreen.SkipStepOne();
            topScreen.SkipStepOne();
        }

        #endregion

        #region ParticleGeneration

        private List<Particle> GenerateParticles()
        {
            List<Reaction> currentReactions = SelectReactions();
            generatedParticles = currentReactions.SelectMany(reaction => reaction.exit.particles).ToList();
            DisplayParticles(generatedParticles);
            return generatedParticles;
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

        #endregion


        //Exile loading screen

        /// <summary>
        /// Tells the tablet that the experiment has finished loading.
        /// </summary>
        public void LoadingBarFinished()
        {
            tabletScreen.WaitingConfirmation();
        }

        public void StartParticleIdentification()
        {
            holograms[0].ActivateHologram(true);
            holograms[0].StartAnimation();
            topScreen.StartParticleIdentification();
            tabletScreen.StartParticleIdentification();
        }

        /// <summary>
        /// Activates the manual override panel of the tablet.
        /// </summary>
        public void ActivateManualOverride()
        {
            tabletScreen.ManualOverride();
        }

        public void StartAdvancedManualOverride()
        {
            
            topScreen.StartAnalysisAnimation();
            tabletScreen.OverrideSecond();
        }

        public void StartReactionIdentification()
        {
            topScreen.StartReactionIdentification();
        }


        //Reaction identification screen

        //TODO: obsolete second part of experiment?
        /// <summary>
        /// Converts the produced particles into a string.
        /// </summary
        /*private void GenerateParticleString()
        {
            CorrectParticle();
        }*/

        //TODO: obsolete second part experiment?
        /// <summary>
        /// Tells the main screen that the correct combination of particles has been entered.
        /// </summary>
        public void CorrectParticle()
        {
            //_topScreen.reactionIdentification():
            //_holograms[0].DisplaySplines();
        }


        protected override void PreShow(VirtualWallTopZone wallTopZone, ElementInfo[] zones)
        {
            base.PreShow(wallTopZone, zones);
            holograms[0].DisplaySplines();
        }


        protected override void PreActivate()
        {
            base.PreActivate();
            GenerateParticles();
        }

        protected override void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, XPState stateOnActivation)
        {
            base.PostInit(xpContext, info, logController, stateOnActivation);
            holograms = GetElements<MAIAHologram>();
            tabletScreen = GetElement<MAIATabletScreen>();
            topScreen = GetElement<MAIATopScreen>();
            tubeScreen = GetElement<MAIATubeScreen>();
            tabletScreen.tubeScreen = tubeScreen;
            tabletScreen.hologram = holograms[0];
            topScreen.tabletScreen = tabletScreen;
            settings = (MAIASettings)xpContext.xpSettings;
        }
    }
}
