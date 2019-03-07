using CRI.HelloHouston.Calibration;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

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
        /// The hologram tube of the table block.
        /// </summary>
        public MAIAHologramTube hologramTube { get; private set; }
        /// <summary>
        /// The hologram Feynman of the table block.
        /// </summary>
        private MAIAHologramFeynman _hologramFeynman;
        /// <summary>
        /// The bottomscreen script of the experiment block.
        /// </summary>
        private MAIABottomScreen _bottomScreen;
        /// <summary>
        /// Settings of the experience.
        /// </summary>
        public MAIASettings settings
        {
            get
            {
                return (MAIASettings)xpContext.xpSettings;
            }
        }
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
        private System.Random _rand;

        #region GameMasterActions

        /// <summary>
        /// Skips to the second part od the experiment.
        /// </summary>
        public override void SkipToStep(int step)
        {
            base.SkipToStep(step);
            XPStepManager.StepAction stepAction = stepManager.currentStepAction;
            stepAction.action();
        }

        internal void StartHologramTubeAnimation()
        {
            hologramTube.StartAnimation();
        }


        internal void ResetHologram()
        {
            if (hologramTube.isActiveAndEnabled)
                hologramTube.StartAnimation();
            if (_hologramFeynman.isActiveAndEnabled)
                _hologramFeynman.ResetPositions();
        }
        #endregion

        #region ParticleGeneration

        /// <summary>
        /// Generates the list of particles detected for this game.
        /// </summary>
        /// <returns>The list of detected particles.</returns>
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
            Debug.Log("test");
            ongoingReactions = settings.allReactions
                .Where(reaction => reaction.fundamental)
                .OrderBy(reaction => _rand.Next())
                .Take(settings.reactionCount)
                .ToList();
            selectedReaction = ongoingReactions[_rand.Next(0, settings.reactionCount)];
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
        /// <summary>
        /// Activates the manual override panel of the tablet.
        /// </summary>
        public void OnLoadingSuccess()
        {
            tabletScreen.StartMO();
        }

        public void OnMOSuccess()
        {
            stepManager.AdvanceStep("MO");
            topScreen.InitPasswordInput();
        }

        public void OnPasswordSuccess()
        {
            stepManager.AdvanceStep("Password");
            hologramTube.ActivateHologram(true);
            topScreen.StartPI();
            tabletScreen.StartPI();
        }

        public void OnPISuccess()
        {
            stepManager.AdvanceStep("PI");
            topScreen.StartAnalysisAnimation();
            tabletScreen.HideAllPanels();
            hologramTube.gameObject.SetActive(false);
        }

        public void OnAnalysisAnimationFinished()
        {
            tabletScreen.StartAMO();
        }

        public void OnAMOSuccess()
        {
            stepManager.AdvanceStep("AMO");
            topScreen.StartRI();
            tabletScreen.StartRI();
            _hologramFeynman.gameObject.SetActive(true);
            _hologramFeynman.FillBoxesDiagrams();
        }

        internal void OnRISuccess()
        {
            stepManager.AdvanceStep("RI");
            topScreen.Victory();
            tabletScreen.Victory();
        }

        public XPStepManager.StepAction[] InitStepActions()
        {
            return new XPStepManager.StepAction[]
            {
                new XPStepManager.StepAction("Start", 0, OnLoadingSuccess),
                new XPStepManager.StepAction("MO", 1, OnLoadingSuccess),
                new XPStepManager.StepAction("Password", 2, OnMOSuccess),
                new XPStepManager.StepAction("PI", 6, OnPasswordSuccess),
                new XPStepManager.StepAction("AMO", 2, OnPISuccess),
                new XPStepManager.StepAction("RI", 6, OnAMOSuccess),
                new XPStepManager.StepAction("Finish", 0, OnRISuccess)
            };
        }


        protected override void PreActivate()
        {
            base.PreActivate();
            GenerateParticles();
        }

        protected override void PostHide()
        {
            base.PostHide();
            tabletScreen = null;
            topScreen = null;
            tubeScreen = null;
        }

        protected override void PreShow(VirtualWallTopZone wallTopZone, ElementInfo[] info)
        {
            base.PreShow(wallTopZone, info);
            tabletScreen = GetElement<MAIATabletScreen>();
            topScreen = GetElement<MAIATopScreen>();
            tubeScreen = GetElement<MAIATubeScreen>();
            topScreen.tabletScreen = tabletScreen;
        }

        protected override void PostShow(VirtualWallTopZone wallTopZone, ElementInfo[] zones)
        {
            base.PostShow(wallTopZone, zones);
            hologramTube.DisplayAllSplines();
        }

        protected override void PreInit(XPContext xpContext, LogExperienceController logController, int randomSeed, XPState stateOnActivation)
        {
            base.PreInit(xpContext, logController, randomSeed, stateOnActivation);
            _rand = new System.Random(randomSeed);
        }

        protected override void PostInit(XPContext xpContext, ElementInfo[] info, LogExperienceController logController, int randomSeed, XPState stateOnActivation)
        {
            base.PostInit(xpContext, info, logController, randomSeed, stateOnActivation);
            stepManager.stepActions = InitStepActions();
            hologramTube = GetElement<MAIAHologramTube>();
            _hologramFeynman = GetElement<MAIAHologramFeynman>();
            _bottomScreen = GetElement<MAIABottomScreen>();
        }
    }
}
