using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class ReactionsIdentification : MonoBehaviour
    {
        /// <summary>
        /// Script for the whole top screen.
        /// </summary>
        [SerializeField]
        private MAIATopScreen _maiaTopScreen = null;
        /// <summary>
        /// Slots where the diagrams of the chosen reactions are displayed.
        /// </summary>
        [SerializeField]
        private Image[] _diagrams = null;
        /// <summary>
        /// Displays the type of interaction of the real reaction.
        /// </summary>
        [SerializeField]
        private Text _textInteraction = null;
        /// <summary>
        /// Image to be displayed insted of the Feynman diagram for the real reaction.
        /// </summary>
        [SerializeField]
        private Sprite _diagramMissing = null;
        /// <summary>
        /// Popups displayed when the right or wrong Feynman diagram.
        /// </summary>
        [SerializeField]
        private GameObject _popupWin = null;
        /// <summary>
        /// Popup displayed when selecting the wrong Feynman diagram.
        /// </summary>
        [SerializeField]
        private GameObject _popupLose = null;
        /// <summary>
        /// Error popup for the diagram selection.
        /// </summary>
        [SerializeField]
        private GameObject _popupErrorRessource = null;
        /// <summary>
        /// Error popup for the diagram selection.
        /// </summary>
        [SerializeField]
        private GameObject _popupErrorNumber = null;
        /// <summary>
        /// An error depending on the player's diagram selection mistake.
        /// </summary>
        [HideInInspector]
        public string particleErrorString;
        [SerializeField]
        private Text[] _particleTexts;

        /// <summary>
        /// Effect if the correct Feynman diagram is selected, or a wrong one.
        /// </summary>
        /// <param name="realReaction">The real reaction.</param>
        /// <param name="reactionSelected">The reaction selected by the player.</param>
        public void ReactionSelected(Reaction realReaction, Sprite reactionSelected)
        {
            if (reactionSelected == realReaction.diagramImage)
            {
                _popupWin.SetActive(true);
            }
            else
            {
                _popupLose.SetActive(true);

                StartCoroutine(_maiaTopScreen.WaitGeneric(2f, () =>
                {
                    _popupLose.SetActive(false);
                }));
            }

        }

        /// <summary>
        /// Fills the interaction type of the real reaction on the interaction type window.
        /// </summary>
        /// <param name="chosenReaction">The chosen reaction.</param>
        public void FillInteractionType(Reaction chosenReaction)
        {
            _textInteraction.text = chosenReaction.entries.ToString();
        }

        /// <summary>
        /// Displays the Feynman diagrams of each chosen reaction except the real one.
        /// </summary>
        /// <param name="reactions"></param>
        /// <param name="chosenReaction"></param>
        public void FillChosenDiagrams(List<Reaction> reactions, Reaction chosenReaction)
        {
            int i = 0;

            List<Reaction> reactionsTemp = new List<Reaction>();

            foreach (Reaction reaction in reactions)
            {
                reactionsTemp.Add(reaction);
            }

            while (reactionsTemp.Count != 0)
            {
                if (reactionsTemp[0].diagramImage != chosenReaction.diagramImage)
                {
                    _diagrams[i].sprite = reactionsTemp[0].diagramImage;
                }
                else
                {
                    _diagrams[i].sprite = _diagramMissing;
                }
                reactionsTemp.RemoveAt(0);
                i++;
            }
        }

        /// <summary>
        /// Fills the number of each kind of particles in the particles table.
        /// </summary>
        /// <param name="nbParticles">The number of particles of the kind.</param>
        /// <param name="entry">The text to be filled.</param>
        public void FillParticlesTable(List<Particle> particles)
        {
            foreach (var particleGroup in particles.GroupBy(particle => particle.particleName))
            {
                foreach(Text particleText in _particleTexts)
                {
                    if(particleGroup.Key == particleText.name)
                    {
                        particleText.text = particleGroup.Count().ToString();
                    }
                }
            }
        }
    }
}
