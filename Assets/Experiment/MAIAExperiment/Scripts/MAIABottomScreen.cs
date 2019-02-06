using System;
using System.Collections;
using System.Collections.Generic;
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
        public class EntriesSprite
        {
            /// <summary>
            /// Value of a reaction EntryType.
            /// </summary>
            public EntryType type;
            /// <summary>
            /// Sprite of a post-it to be displayed on the bottom screen.
            /// </summary>
            public Sprite sprite;
        }
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        public MAIAManager manager { get; private set; }
        /// <summary>
        /// Contains all the possible Entrytype-sprite associations.
        /// </summary>
        public EntriesSprite[] interactionDiagrams;
        /// <summary>
        /// The screen on which the post-it should be displayed.
        /// </summary>
        [SerializeField]
        private Image screen;

        /// <summary>
        /// Displays the post-it depending on the chosen reaction for this game.
        /// </summary>
        public void DisplayInteraction()
        {
            foreach(EntriesSprite entrySprite in interactionDiagrams)
            {
                if(manager.selectedReaction.entries == entrySprite.type)
                {
                    screen.sprite = entrySprite.sprite;
                    break;
                }
            }
        }

        private void Init(MAIAManager synchronizer)
        {
            manager = synchronizer;
        }

        public override void OnShow()
        {
            
        }

        public override void OnActivation(XPManager manager)
        {
            Init((MAIAManager)manager);
            base.OnActivation(manager);
            DisplayInteraction();
        }
    }
}
