using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIABottomScreen : XPElement
    {
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        public MAIAManager manager { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Sprite[] interactionDiagrams;
        [SerializeField]
        private Image screen;

        /// <summary>
        /// 
        /// </summary>
        public void DisplayInteraction()
        {
            screen.sprite = interactionDiagrams[0];
        }

        private void Init(MAIAManager synchronizer)
        {
            manager = synchronizer;
        }

        public override void OnActivation(XPManager manager)
        {
            Init((MAIAManager)manager);
            base.OnActivation(manager);
        }
    }
}
