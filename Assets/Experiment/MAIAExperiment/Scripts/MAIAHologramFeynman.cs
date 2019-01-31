using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CRI.HelloHouston.Experience.MAIA
{
    public class MAIAHologramFeynman : XPHologramElement
    {
        /// <summary>
        /// The synchronizer of the experiment.
        /// </summary>
        public MAIAManager manager { get; private set; }
        /// <summary>
        /// The objects containing the Feynman diagrams.
        /// </summary>
        public GameObject[] feynmanBoxes;
        /// <summary>
        /// Name of the Feynman digram chosen by the player.
        /// </summary>
        public string feynmanBoxName;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        public void SkipStepOne()
        {
            FillBoxesDiagrams();
        }

        /// <summary>
        /// 
        /// </summary>
        public void FillBoxesDiagrams()
        {
            feynmanBoxes = GameObject.FindGameObjectsWithTag("Feynmanbox");
            int i = 0;
            foreach (Reaction reaction in manager.settings.allReactions)
            {
                MeshRenderer[] renderers = feynmanBoxes[i].GetComponentsInChildren<MeshRenderer>();
                renderers[1].material.mainTexture = reaction.diagramImage;
                i++;
            }
        }

        private void Init(MAIAManager synchronizer)
        {
            manager = synchronizer;
        }

        public override void OnActivation(XPManager manager)
        {
            Init((MAIAManager)manager);
            base.OnActivation(manager);
            gameObject.SetActive(false);
        }
    }
}
