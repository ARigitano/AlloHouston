using CRI.HelloHouston.Calibration;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

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
        public Particle[] _allParticles { get; private set; }
        /// <summary>
        /// All the reaction scriptable objects.
        /// </summary>
        public Reaction[] allReactions { get; private set; }
        /// <summary>
        /// Path to the particle scriptable objects folder.
        /// </summary>
        [HideInInspector]
        public const string path = "Particles";
        /// <summary>
        /// Path to the particle scriptable objects folder.
        /// </summary>
        [HideInInspector]
        public const string pathReaction = "Reactions";
        //TODO: check if useful
        /// <summary>
        /// Contains the combination of particles randomly generated.
        /// </summary>
        public Particle[] generatedParticles { get; private set; }
        //TODO: put in settings
        /// <summary>
        /// Real password to get access.
        /// </summary>
        public string realPassword;
        /// <summary>
        /// The combination of particles randomly generated rewritten as a string.
        /// </summary>
        public List<string> realParticles { get; private set; }
        //TODO: see if used
        /// <summary>
        /// String displayed depending on the particles combination entered.
        /// </summary>
        public string[] result { get; private set; }
        //TODO: put in settings
        /// <summary>
        /// Number of ongoing reactions.
        /// </summary>
        public int numberChosenReaction = 4;
        /// <summary>
        /// The ongoing reactions.
        /// </summary>
        public List<Reaction> chosenReactions { get; private set; }
        /// <summary>
        /// The reaction to idetify.
        /// </summary>
        public Reaction realReaction { get; private set; }
        //TODO: check if useful
        /// <summary>
        /// The particles produced by the ongoing reactions.
        /// </summary>
        public List<Particle> reactionExits { get; private set; }
        //TODO: store? + linc.groupBy.Count()
        /// <summary>
        /// The numbers of particle detected of each kind.
        /// </summary>
        [HideInInspector]
        public int nbQuark, nbAntiquark, nbMuon, nbAntimuon, nbElectron, nbAntielectron, nbNeutrino, nbPhoton;

    //TODO: getelement in init
    private void Start()
        {
            _tabletScreen.tubeScreen = _tubeScreen;
            _tabletScreen.topScreen = _topScreen;
            _tabletScreen.hologram = _holograms[0];
            _topScreen.tabletScreen = _tabletScreen;
        }

        /// <summary>
        /// Selects the ongoing particle reactions for this game.
        /// </summary>
        private void ReactionsCombination()
        {
            allReactions = Resources.LoadAll<Reaction>(pathReaction);

            List<Reaction> fundamentals = new List<Reaction>();

            foreach (Reaction reaction in allReactions)
            {
                if (reaction.fundamental)
                {
                    fundamentals.Add(reaction);
                }
            }

            for (int i = 0; i < numberChosenReaction; i++)
            {
                int randNumber = UnityEngine.Random.Range(0, fundamentals.Count);
                chosenReactions.Add(fundamentals[randNumber]);
                fundamentals.RemoveAt(randNumber); ;
            }

            realReaction = chosenReactions[UnityEngine.Random.Range(0, chosenReactions.Count)];
            logController.AddLog(realReaction.name, xpContext);
        }

        /// <summary>
        /// Counts the number of particles detetected of each kind.
        /// </summary>
        /// <param name="particles">The particles detected.</param>

        public void CountParticles(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                switch (particle.symbol)
                {

                    case "q":
                        nbQuark++;
                        break;
                    case "qBar":
                        nbAntiquark++;
                        break;
                    case "μ":
                        nbMuon++;
                        break;
                    case "μBar":
                        nbAntimuon++;
                        break;
                    case "e":
                        nbElectron++;
                        break;
                    case "eBar":
                        nbAntielectron++;
                        break;
                    case "v":
                        nbNeutrino++;
                        break;
                    case "vBar":
                        nbNeutrino++;
                        break;
                    case "γ":
                        nbPhoton++;
                        break;
                    default:
                        break;
                }
            }
            logController.AddLog("Quarks:" + nbQuark, xpContext);
            logController.AddLog("Antiquarks:" + nbAntiquark, xpContext);
            logController.AddLog("Muons:" + nbMuon, xpContext);
            logController.AddLog("Antimuons:" + nbAntimuon, xpContext);
            logController.AddLog("Electrons:" + nbElectron, xpContext);
            logController.AddLog("Antielectrons:" + nbAntielectron, xpContext);
            logController.AddLog("Neutrinos:" + nbNeutrino, xpContext);
            logController.AddLog("Photons:" + nbPhoton, xpContext);
        }


        /// <summary>
        /// Lists the particles produced by the ongoing reactions.
        /// </summary>
        /// <returns>The list of produced particles.</returns>
        public List<Particle> ParticlesCombination()
        {
            _allParticles = Resources.LoadAll<Particle>(path);

            ReactionsCombination();

            foreach (Reaction reaction in chosenReactions)
            {
                string[] particlesStrings = reaction.exits.ToString().Split('_');

                for (int i = 0; i < particlesStrings.Length; i++)
                {
                    foreach (Particle particle in _allParticles)
                    {
                        if (particle.symbol == particlesStrings[i])
                        {
                            reactionExits.Add(particle);
                            logController.AddLog(particle.particleName, xpContext);
                            realParticles.Add(particle.symbol);
                        }
                    }
                }


            }

            CountParticles(reactionExits);
            GenerateParticleString();
            return reactionExits;
        }

        //TODO: delete but call correctparticle somewhere else
        /// <summary>
        /// Converts the produced particles into a string.
        /// </summary
        private void GenerateParticleString()
        {
            string type = "";


            for (int i = 0; i < realParticles.Count; i++)
            {
                type += realParticles[i];
            }
            CorrectParticle();
        }

        /// <summary>
        /// Activates the manual override panel of the tablet.
        /// </summary>
       public void ManualOverrideActive()
        {
            _topScreen.ManualOverride();
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

            if (realParticles.Count == 0)
                ParticlesCombination();
        }

        /// <summary>
        /// Tells the tablet that the experiment has finished loading.
        /// </summary>
        public void LoadingBarFinished()
        {
            _tabletScreen.WaitingConfirmation();
        }

        

        /// <summary>
        /// Tells the main screen that the correct combination of particles has been entered.
        /// </summary>
        public void CorrectParticle()
        {
           //TODO:rewrite
            _holograms[0].AnimHologram(reactionExits);
            _holograms[0].DisplaySplines();
            _topScreen.ParticleGrid(reactionExits);
            /*_topScreen.FillParticlesTable(nbAntielectron, _textAntielectron);
            _topScreen.FillParticlesTable(nbAntimuon, _textAntimuon);
            _topScreen.FillParticlesTable(nbAntiquark, _textAntiquark);
            _topScreen.FillParticlesTable(nbElectron, _textElectron);
            _topScreen.FillParticlesTable(nbMuon, _textMuon);
            _topScreen.FillParticlesTable(nbNeutrino, _textNeutrino);
            _topScreen.FillParticlesTable(nbPhoton, _textPhoton);
            _topScreen.FillParticlesTable(nbQuark, _textQuark);
            _topScreen.FillChosenDiagrams(_chosenReactions, _realReaction);
            _topScreen.FillInteractionType(_realReaction);*/
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
        }

        internal void generateExits()
        {
            reactionExits = ParticlesCombination();
        }
    }
}
