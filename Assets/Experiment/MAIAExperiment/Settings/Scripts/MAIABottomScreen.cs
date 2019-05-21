using CRI.HelloHouston.Translation;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    /// <summary>
    /// The bottom element for the MAIA experiment.
    /// </summary>
    public class MAIABottomScreen : XPElement
    {
        /// <summary>
        /// Class to assign the value of a reaction EntryType to the sprite of a post-it.
        /// </summary>
        [Serializable]
        public struct EntriesSprite
        {
            /// <summary>
            /// Value of a reaction EntryType.
            /// </summary>
            [Tooltip("Value of a reaction EntryType.")]
            public EntryType type;
            /// <summary>
            /// TranslatedImageAsset of a post-it to be displayed on the bottom screen.
            /// </summary>
            [Tooltip("TranslatedImageAsset of a post-it to be displayed on the bottom screen.")]
            public TranslatedImageAsset translatedImage;
        }
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        public MAIAManager maiaManager { get; private set; }
        /// <summary>
        /// Contains all the possible Entrytype-sprite associations.
        /// </summary>
        public EntriesSprite[] interactionDiagrams;
        /// <summary>
        /// The screen on which the post-it should be displayed.
        /// </summary>
        [SerializeField]
        private TranslatedImage screen = null;

        /// <summary>
        /// Displays the post-it depending on the chosen reaction for this game.
        /// </summary>
        public void DisplayInteraction()
        {
            foreach(EntriesSprite entrySprite in interactionDiagrams)
            {
                if(maiaManager.selectedReaction.entries == entrySprite.type)
                {
                    screen.InitTranslatedText(manager, entrySprite.translatedImage);
                    break;
                }
            }
        }

        private void Init(MAIAManager manager)
        {
            Debug.Log(manager);
            maiaManager = manager;
        }

        public override void OnInit(XPManager manager, int randomSeed)
        {
            base.OnInit(manager, randomSeed);
            Init((MAIAManager)manager);
        }

        public override void OnActivation()
        {
            base.OnActivation();
            DisplayInteraction();
        }
    }
}
